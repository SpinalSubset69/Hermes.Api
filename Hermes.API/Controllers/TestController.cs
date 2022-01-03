using Core.entities;
using Hermes.API.Dtos;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Controllers
{
    public class TestController : BaseController
    {
        private readonly IUnitOfWork _unitOFWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOFWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories([FromQuery] CategorySpecParams categoryParams)
        {
            var spec = new CategorySpecifications(categoryParams);
            var countSpec = new CategoryForCountWithSpecifications(categoryParams);
            var count = await _unitOFWork.Categories.GetCountWithSpecifications(countSpec);
            var categories = await _unitOFWork.Categories.ListAllAsync(spec);
            return new JsonResult(new { message = "Funciono", data = new Pagination<Category>(count, categoryParams.PageSize, categoryParams.PageIndex, categories)});
        }
    }
}
