using Core.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Core.Interfaces.Specifications.Categories
{
    public class CategorySpecifications : BaseSpecification<Category>
    {   
        public CategorySpecifications(CategorySpecParams categoryParams)
            :base(x => 
                (string.IsNullOrEmpty(categoryParams.Category) || x.Name.ToLower().Contains(categoryParams.Category.ToLower())) &&
                (!categoryParams.CategoryId.HasValue || x.Id == categoryParams.CategoryId)
            ) 
                
        {
            ApplyPagging((categoryParams.PageIndex - 1) * categoryParams.PageSize, categoryParams.PageSize); 
        }
    }
}
