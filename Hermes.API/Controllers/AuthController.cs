using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(AuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetReporterFromToken()
        {
            try
            {
                var headerToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                i
                    f (!headerToken.Contains("bearer"))
                {
                    return new JsonResult(new { message = "Must provided a BEARER" });
                }

                var reporter = await _authService.GetReporterbasedOnTokenAsync(headerToken);

                return ResponseWithData<ReporterToReturnDto>.HttpResponseWithData("Reporter", reporter);
            }catch(Exception ex)
            {
                return HttpHandleErrors("Error on server", ex);
            }
        }      

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] LoginDto loginInfo)
        {
            try
            {
                var session = await _authService.LoginAsync(loginInfo);

                return new JsonResult(new
                {
                    message = "Login Succesfully",
                   session = new
                   {
                       token = session.Token,
                       expiresIn = session.ExpiresIn
                   }
                });
            }catch(Exception ex)
            {
                return HttpHandleErrors("Error", ex);
            }
        }
    }
}
