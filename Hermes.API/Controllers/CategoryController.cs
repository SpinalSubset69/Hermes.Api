using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Util;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Controllers
{
    
    public class CategoryController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery]CategorySpecParams categoryParams)
        {
            var spec = new CategorySpecifications(categoryParams);
            var countSpec = new CategoryForCountWithSpecifications(categoryParams);

            var count = await _unitOfWork.Categories.GetCountWithSpecifications(countSpec);
            var categories = await _unitOfWork.Categories.ListAllAsync(spec);

            return ResponseWithPagination<Category>.HttpResponseWithPagination(
                new Pagination<Category>(count, categoryParams.PageSize, categoryParams.PageIndex, categories));
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostCategory([FromBody] RegisterCategory categoryFromBody)
        {
            var categoryOnDb = await _unitOfWork.Categories.FindByNameAsync(categoryFromBody.Name);
            if(categoryOnDb != null)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    message = "Category Already Exists"
                });
            }
            var category = _mapper.Map<RegisterCategory, Category>(categoryFromBody);
            _unitOfWork.Categories.SaveEntity(category);
            await _unitOfWork.SaveChangesAsync();

            return ResponseWithData<Category>.HttpResponseWithData("Category Saved", category);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<JsonResult> UpateCategory(int id, [FromBody]RegisterCategory categoryFromBody)
        {
            var category = await _unitOfWork.Categories.FindByIdAsync(id);
            category.Name = categoryFromBody.Name;
            _unitOfWork.Categories.UpdateEntity(category);
            await _unitOfWork.SaveChangesAsync();
            return new JsonResult(new
            {
                message = "Category Updated"
            });
        }
        
    }
}
