using Core.entities;
using Hermes.Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Repositories
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQueryable(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Critetria != null) 
            {
                query = query.Where(spec.Critetria);
            }

            if (spec.isPaggingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

           
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
         

            return query;
        }
    }
}
