using Hermes.API.Services;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Reporters;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthMiddleware(RequestDelegate next, AuthService authService, IUnitOfWork untiOfWork)
        {
            _next = next;
            _authService = authService;
            _unitOfWork = untiOfWork;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["X-Access-Token"].FirstOrDefault()?.Split(" ").Last();
            
            if (token != null)
            {
                try
                {
                    var session = _authService.VerifyJwt(token);
                    var spec = new ReporterWithIncludesByIdSpecification(Convert.ToInt32(session.Issuer));
                    var reporter = await _unitOfWork.Reporters.FindByIdAsync(spec);
                    context.Items["Reporter"] = reporter;
                }
                catch(Exception ex)
                {
                    context.Items["Unauthorized"] = ex.Message;
                }

            }
            context.Items["Unauthorized"] = "Must Provide A Token";
            await _next(context);
        }
     
    }
}
