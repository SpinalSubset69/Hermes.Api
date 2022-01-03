using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Reporters
{
    public  class ReporterWithIncludesByIdSpecification : BaseSpecification<Reporter>
    {
        public ReporterWithIncludesByIdSpecification(int id)
            :base(x => x.Id == id)
        {
            AddInclude(x => x.Articles);
            AddInclude(x => x.Role);
        }
    }
}
