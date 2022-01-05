using Hermes.API.Extensions;
using Hermes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Services Extensions
builder.Services.ServiceExtensionsMethod(builder.Configuration);

//Db Context
builder.Services.AddDbContext<HermesDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("dev"));
});
//DbContext

var app = builder.Build();

//Seed Database
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<HermesDbContext>();
        await context.Database.MigrateAsync();
        await ContextSeed.SeedDataAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error Seeding Database");
    }
}
//Seed Database

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

//Must go above authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
        
app.Run();
