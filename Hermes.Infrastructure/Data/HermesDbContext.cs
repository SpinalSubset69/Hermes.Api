using Core.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Data
{
    public class HermesDbContext : DbContext
    {
        public HermesDbContext(DbContextOptions<HermesDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Role> Roles { get; set; }  
        public DbSet<Reporter> Reporters { get; set; }  
        public DbSet<Image> Images { get; set; }
    }
}
