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
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        private readonly HermesDbContext _context;
        public ArticleRepository(HermesDbContext context)
            :base(context)
        {
            _context = context;
        }

        public Task<Article> FindArticleByIdAsync(int id)
        {
            return _context.Articles.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
