using Core.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Data
{
    public class ContextSeed
    {
        public static async Task SeedDataAsync(HermesDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../Hermes.Infrastructure/Data/SeedData/Categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach(var category in categories)
                    {
                        context.Categories.Add(category);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Roles.Any())
                {
                    var rolesData = File.ReadAllText("../Hermes.Infrastructure/Data/SeedData/Rol.json");
                    var roles = JsonSerializer.Deserialize<List<Role>>(rolesData);

                    foreach(var role in roles)
                    {
                        context.Roles.Add(role);
                    }

                    await context.SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<HermesDbContext>();
                logger.LogError(ex, "Error Seeding DataBase");
            }
        }
    }
}
