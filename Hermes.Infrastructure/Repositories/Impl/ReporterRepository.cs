using Core.entities;
using Hermes.Core.Interfaces.Repositories;
using Hermes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Repositories.Impl
{
    public class ReporterRepository : GenericRepository<Reporter>, IReporterRepository
    {
        private readonly HermesDbContext _context;
        public ReporterRepository(HermesDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Reporter> GetReporterByEmail(string email)
        {
            return await _context.Reporters.Where(r => r.Email == email).Include(x => x.Role).FirstOrDefaultAsync();
        }
    }
}
