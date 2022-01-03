using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Services;
using Hermes.API.Util;
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
                var reporter = (Reporter)HttpContext.Items["Reporter"];

                return ResponseWithData<ReporterToReturnDto>.HttpResponseWithData("Reporter", _mapper.Map<Reporter, ReporterToReturnDto>(reporter));
            }catch(Exception ex)
            {
                return HttpHandleErrors("Error on server", ex);
            }
        }

        [HttpGet("verify")]
        public async Task<JsonResult> VerifyToken()
        {
            try
            {
                string token = Request.Headers["X-Access-Token"].FirstOrDefault()?.Split(" ").Last();
                _authService.VerifyJwt(token);

                return new JsonResult(new { message = "Authorized" });
            }catch(Exception ex)
            {
                return HttpHandleErrors("Error on Server", ex);
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
