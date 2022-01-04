using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Util;
using Hermes.API.Util.Exceptions;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Reporters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Hermes.API.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private string securityKey;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            securityKey = _config.GetSection("Cryptography").GetSection("SecurityKey").Value;
            _mapper = mapper;
        }

        public string GenerateJwt(int id)
        {
            var byteSymmetricSecurityKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(byteSymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Now.AddMinutes(30));
            var securityToken = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken VerifyJwt(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = UTF8Encoding.UTF8.GetBytes(securityKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        public async Task<ReporterToReturnDto> GetReporterbasedOnTokenAsync(string jwt)
        {
            var sessionToken = VerifyJwt(jwt);
            var spec = new ReporterWithIncludesByIdSpecification(Convert.ToInt32(sessionToken.Issuer));
            var reporter = await _unitOfWork.Reporters.FindByIdAsync(spec);

            if(reporter == null)
            {
                throw new NotFoundException("Reporter Not Found");
            }

            return _mapper.Map<Reporter, ReporterToReturnDto>(reporter);
        }

        public async Task<SessionDto> LoginAsync(LoginDto loginInfo)
        {
            var reporter = await _unitOfWork.Reporters.GetReporterByEmail(loginInfo.Email);

            if(reporter == null)
            {
                throw new NotFoundException("Reporter Not Found");
            }

            if(reporter.Password != Encrypt.GetSha256(loginInfo.Password))
            {
                throw new ApplicationException("Invalid Credentials");
            }

            var token = GenerateJwt(reporter.Id);
            var expiresIn = DateTimeOffset.Now.AddMinutes(30).ToUnixTimeMilliseconds();

            var session = new SessionDto(token, expiresIn.ToString());
            return session;
        }

        public long millis()
        {
            return (long.MaxValue + DateTime.Now.ToBinary()) / 10000;
        }
    }
}

