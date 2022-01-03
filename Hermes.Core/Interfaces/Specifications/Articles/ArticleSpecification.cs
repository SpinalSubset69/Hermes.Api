using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Articles
{
    public class ArticleSpecification : BaseSpecification<Article>
    {
        public ArticleSpecification(ArticleSpecParams articleParams)
            : base(x =>
                (!articleParams.ArticleId.HasValue || x.Id == articleParams.ArticleId) &&
                (!articleParams.ReporterId.HasValue || x.ReporterId == articleParams.ReporterId) &&
                (string.IsNullOrEmpty(articleParams.Query) || x.Title.ToLower().Contains(articleParams.Query) || x.Content.ToLower().Contains(articleParams.Query))
            )
        {
            AddInclude(x => x.Images);
            AddInclude(x => x.Reporter);
            AddInclude(x => x.Category);

            ApplyPagging((articleParams.PageIndex - 1) * articleParams.PageSize, articleParams.PageSize);
        }
    }
}
