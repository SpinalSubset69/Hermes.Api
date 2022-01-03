using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Categories
{
    public class CategorySpecParams : BaseSpecParams
    {
        private string _category;
        public string? Category
        {
            get => _category;
            set => _category = value.ToLower();
        }
        public int? CategoryId { get; set; }
    }
}
