using Hermes.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IRoleRepository Roles { get; }  
        IReporterRepository Reporters { get; }
        IArticleRepository Articles { get; }    
        IImagesRepository Images { get; }
        Task SaveChangesAsync();
    }
}
