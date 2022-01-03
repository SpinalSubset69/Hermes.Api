using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Reporters
{
    public class ReporterSpecParams
    {
        public string? Email { get; set; }
        public string? Reporter { get; set; }
        public int? ReporterId { get; set; }
    }
}
