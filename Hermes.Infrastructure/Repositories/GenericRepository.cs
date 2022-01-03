using Core.entities;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications;
using Hermes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hermes.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HermesDbContext _context;

        public GenericRepository(HermesDbContext context)
        {
            _context = context;
        }

        public async Task<T> FindByIdAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstAsync<T>();
        }

        public async Task<int> GetCountWithSpecifications(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<IEnumerable<T>> ListAllAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();            
        }

        public void RemoveEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void SaveEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Update(entity);            
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {           
         return SpecificationEvaluator<T>.GetQueryable(_context.Set<T>().AsQueryable() ,spec);            
        }
    }
}
