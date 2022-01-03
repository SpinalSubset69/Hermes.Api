using Core.entities;
using Hermes.Core.Interfaces.Repositories;
using Hermes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Repositories.Impl
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly HermesDbContext _context;
        public CategoryRepository(HermesDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await _context.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
