using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Reporters
{
    public class ReporterWithRoleSpecification : BaseSpecification<Reporter>
    {
        
        public ReporterWithRoleSpecification(ReporterSpecParams reporterParams)
            : base(x =>
                    (!reporterParams.ReporterId.HasValue || x.Id == reporterParams.ReporterId) && 
                    (string.IsNullOrEmpty(reporterParams.Reporter) || x.Name.ToLower().Contains(reporterParams.Reporter.ToLower()))
                 )
        {
            AddInclude(x => x.Role);
        }
        public ReporterWithRoleSpecification()
        {
            AddInclude(x => x.Role);
        }
    }
}
