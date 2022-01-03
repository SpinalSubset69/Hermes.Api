using Core.entities;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Dtos
{
    public class ResponseWithData<T>  where T : class
    {
        public static JsonResult HttpResponseWithData(string message, T data)
        {
            return new JsonResult(new
            {
                message = message,
                data = data
            });
        }
    }
}
