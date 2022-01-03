using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Critetria { get; }
        List<Expression<Func<T, object>>> Includes {get; set;}
        int Skip { get; set; }
        int Take { get; set; }
        bool isPaggingEnabled { get; }
    }
}
