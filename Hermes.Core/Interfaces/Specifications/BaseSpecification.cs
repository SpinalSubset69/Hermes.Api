using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
                isPaggingEnabled = false;   
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Critetria = criteria;
            isPaggingEnabled = false;
        }
        public Expression<Func<T, bool>> Critetria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public int Skip { get; set; }
        public int Take { get ; set; }

        public bool isPaggingEnabled { get; private set; }  

        protected void ApplyPagging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            isPaggingEnabled = true;    
        }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);  
        }
    }
}
