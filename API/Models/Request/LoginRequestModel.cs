using FluentResults;
using MediatR;

namespace API.Models.Request
{
    public class LoginRequestModel : IRequest<Result<string>>
    {
        public LoginRequestModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
