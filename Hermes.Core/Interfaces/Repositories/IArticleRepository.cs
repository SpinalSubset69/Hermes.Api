using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<Article> FindArticleByIdAsync(int id);
    }
}
