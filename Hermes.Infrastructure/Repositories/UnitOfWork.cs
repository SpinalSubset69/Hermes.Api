using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Repositories;
using Hermes.Infrastructure.Data;
using Hermes.Infrastructure.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HermesDbContext _context;

        public UnitOfWork(HermesDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Roles = new RoleRepository(_context);   
            Reporters = new ReporterRepository(_context);
            Articles = new ArticleRepository(_context); 
            Images = new ImagesRepository(_context);
        }
        public ICategoryRepository Categories { get; private set; }

        public IRoleRepository Roles { get; private set; }

        public IReporterRepository Reporters { get; private set; }

        public IArticleRepository Articles { get; private set; }

        public IImagesRepository Images { get; private set; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
