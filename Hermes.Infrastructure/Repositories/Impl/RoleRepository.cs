using Core.entities;
using Hermes.Core.Interfaces.Repositories;
using Hermes.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Repositories.Impl
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly HermesDbContext _context;
        public RoleRepository(HermesDbContext context): base(context)
        {
            _context = context;
        }
    }
}
