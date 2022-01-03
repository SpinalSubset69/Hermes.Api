using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Articles
{
    public class ArticleSpecParams : BaseSpecParams
    {
        public string? Query { get; set; }
        public int? ArticleId { get; set; }
        public int? ReporterId { get; set; }

    }
}
