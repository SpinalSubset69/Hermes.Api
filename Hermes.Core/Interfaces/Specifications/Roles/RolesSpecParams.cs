using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Roles
{
    public class RolesSpecParams : BaseSpecParams
    {
        public string Role { get; set; }    
        public int? RoleId { get; set; }
    }
}
