using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Roles
{
    public  class RoleSpecification : BaseSpecification<Role>
    {
        public RoleSpecification(RolesSpecParams roleParams)
            : base(x => 
                (string.IsNullOrEmpty(roleParams.Role ) || x.Name.ToLower().Contains(roleParams.Role.ToLower())) &&
                (!roleParams.RoleId.HasValue || x.Id == roleParams.RoleId)
            )
        {
            ApplyPagging((roleParams.PageIndex - 1) * roleParams.PageSize, roleParams.PageSize);
        }
    }
}
