using API.Models.Domain.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Interfaces.Control
{
    public interface IJwtService
    {
        public string Generate(User user);
        public bool Verify(string tokenString);
    }
}
