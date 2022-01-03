using Core.entities;
using Hermes.Core.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> ListAllAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecifications(ISpecification<T> spec);
        void SaveEntity(T entity);
        void UpdateEntity(T entity);
        Task<T> FindByIdAsync(ISpecification<T> spec);
        void RemoveEntity(T entity);
    }
}
