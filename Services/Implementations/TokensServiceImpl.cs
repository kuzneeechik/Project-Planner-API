using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_Planner_API.Services.Implementations
{
    public class TokensServiceImpl : ITokensService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler
            = new JwtSecurityTokenHandler();
        private readonly JwtOptions _jwtOptions;

        public TokensServiceImpl(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GetToken(Guid studentId)
        {
            ClaimsIdentity claims = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, studentId.ToString())
            ]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Expires = DateTime.Now.AddDays(7).ToUniversalTime(),
                SigningCredentials = new(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);

            return _tokenHandler.WriteToken(token);
        }
    }
}
