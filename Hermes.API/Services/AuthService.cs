using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Util;
using Hermes.API.Util.Exceptions;
using Hermes.Core.Interfaces;
using Hermes.Core.Interfaces.Specifications.Reporters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
            securityKey = _config.GetSection("Cryptography:SecurityKey").Value;
            _mapper = mapper;
        }

        public string GenerateJwt(Reporter reporter)
        {
            //CLAIMS
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, reporter.Name),
                new Claim(ClaimTypes.Role, reporter.Role.Name == "Admin" ? "Admin" : "Reporter")
            };


            //Symmetric Security Key
            var byteSymmetricSecurityKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(securityKey));

            //Credentials
            var credentials = new SigningCredentials(byteSymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //ExpiresIn
            var expiresIn = DateTime.Now.AddMinutes(30);

            //Payload
            var payload = new JwtSecurityToken(claims: claims, expires: expiresIn, signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(payload);

            return jwt;
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

            if(!VerifiedpasswordHased(loginInfo.Password, reporter.Password, reporter.Salt))
            {
                throw new ApplicationException("Invalid Credentials");
            }

            var token = GenerateJwt(reporter);
            var expiresIn = DateTimeOffset.Now.AddMinutes(30).ToUnixTimeMilliseconds();

            var session = new SessionDto(token, expiresIn.ToString());
            return session;
        }
       
        private bool VerifiedpasswordHased(string password, string passwordHash, string saltHased)
        {
            using(var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(saltHased)))
            {
                var passswordStream = Encoding.UTF8.GetBytes(passwordHash);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                var sb = new StringBuilder();

                for (int i = 0; i < computedHash.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", computedHash[i]);
                }
                return passwordHash == sb.ToString();
            }
        }
    }
}

