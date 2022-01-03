using Hermes.API.Util.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        public JsonResult HttpHandleErrors(string message, Exception err)
        {
            int statusCode = (err is NotFoundException) ? 404
                            : (err is InvalidParamException) ? 400 : 500;

            Response.StatusCode = statusCode;
            return new JsonResult(new
            {
                statusCode = statusCode,
                message = message,
                errorMessage = err.Message
            });
        }
    }
}
