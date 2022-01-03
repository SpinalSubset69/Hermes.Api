using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Reporters
{
    public class ReporterByEmailSpecification : BaseSpecification<Reporter>
    {
        public ReporterByEmailSpecification(string email)
            : base(x => x.Email == email)
        {

        }
    }
}
