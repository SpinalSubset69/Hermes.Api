using Core.entities;
using Hermes.API.Dtos;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll([FromQuery] RolesSpecParams roleParams)
        {
            var spec = new RoleSpecification(roleParams);
            var countSpec = new RolesForCountSpecification(roleParams);
            var roles = await _unitOfWork.Roles.ListAllAsync(spec);
            var count = await _unitOfWork.Roles.GetCountWithSpecifications(countSpec);


            return ResponseWithPagination<Role>.HttpResponseWithPagination(
                new Pagination<Role>(count, roleParams.PageSize, roleParams.PageIndex, roles));
        }
    }
}
