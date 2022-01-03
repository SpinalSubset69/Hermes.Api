using Core.entities;
using Hermes.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hermes.API.Util
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var reporter = (Reporter)context.HttpContext.Items["Reporter"];
            var message = context.HttpContext.Items["Unauthorized"];

            if(message != null)
            {
                message = message.ToString().Contains("IDX10223") ? "Token Has Expired" 
                            : message.ToString().Contains("IDX12741") ? "Token Malformed" : message;
            }
            if (reporter == null)
            {
                context.Result = new JsonResult(new { message = message }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
