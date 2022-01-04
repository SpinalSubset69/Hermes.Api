using Core.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

                if (!context.Reporters.Any())
                {
                    var adminData = File.ReadAllText("../Hermes.Infrastructure/Data/SeedData/Admin.json");
                    var admins = JsonSerializer.Deserialize<List<Reporter>>(adminData);

                    foreach(var admin in admins)
                    {                        
                        GetHMACSHA512(admin.Password, out string passwordHashed, out string hashedSalt);
                        admin.Password = passwordHashed;
                        admin.Salt = hashedSalt;

                        context.Add(admin);                       
                    }

                    await context.SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<HermesDbContext>();
                logger.LogError(ex, "Error Seeding DataBase");
            }
        }

        private static void GetHMACSHA512(string plainText, out string hashedText, out string hashedSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                var saltStream = hmac.Key;
                var passwordStream = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                var sb = new StringBuilder();

                for (int i = 0; i < saltStream.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", saltStream[i]);
                }

                hashedSalt = sb.ToString();

                sb.Clear();

                for (int i = 0; i < passwordStream.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", passwordStream[i]);
                }

                hashedText = sb.ToString();
            }
        }
    }
}
