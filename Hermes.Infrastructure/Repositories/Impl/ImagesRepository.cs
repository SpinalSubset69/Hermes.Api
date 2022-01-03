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
    public class ImagesRepository : GenericRepository<Image>, IImagesRepository
    {
        private readonly HermesDbContext _context;

        public ImagesRepository(HermesDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
