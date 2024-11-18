using API.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Control
{
    public class JwtService
    {
        private IConfiguration _config;
        public JwtService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Generate(User user)
        {
            string keyString = _config["JWT:Key"] ?? throw new NullReferenceException("There is something wrong with configurations.");
            string issuerString = _config["JWT:Issuer"] ?? throw new NullReferenceException("There is something wrong with configurations.");
            string audienceString = _config["JWT:Key"] ?? throw new NullReferenceException("There is something wrong with configurations.");
            string expiresInMinutesString = _config["JWT:ExpiresInMinutes"] ?? throw new NullReferenceException("There is something wrong with configurations.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = issuerString;
            var audience = audienceString;
            var expires = double.Parse(expiresInMinutesString);

            var claims = new List<Claim>
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expires),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool Verify(string tokenString)
        {
            var token = new JwtSecurityToken(tokenString);
            return true;
        }
    }
}
