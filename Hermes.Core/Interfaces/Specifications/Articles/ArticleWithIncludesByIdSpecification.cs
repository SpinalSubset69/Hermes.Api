using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Articles
{
    public class ArticleWithIncludesByIdSpecification : BaseSpecification<Article>
    {
        public ArticleWithIncludesByIdSpecification(int articleId)
            :base(x => x.Id == articleId)
        {
            AddInclude(x => x.Reporter);
            AddInclude(x => x.Category);
            AddInclude(x => x.Images);
        }
    }
}
