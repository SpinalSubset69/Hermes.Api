using Core.entities;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Dtos
{
    public class ResponseWithPagination<T> where T : class
    {
        public static JsonResult HttpResponseWithPagination(Pagination<T> pagination)
        {
            return new JsonResult(new
            {
             count = pagination.Count,
             pageSize = pagination.PageSize,
             pageIndex = pagination.PageIndex,
             data = pagination.Data
            });
        }
    }
}
