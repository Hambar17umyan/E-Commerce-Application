using API.Models.Control.ResultModels;
using API.Models.Request.Commands;
using API.Services.Concrete.Control;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;
using System.Net;

namespace API.RequestHandlers.CommandHandlers
{
    public class LoginRequestHandler : IRequestHandler<LoginRequestModel, InnerResult<string>>
    {
        private IUserDataService _userDataService;
        private IJwtService _jwtService;
        private IPasswordHashingService _passwordHashingService;
        public LoginRequestHandler(IUserDataService userDataService, IJwtService jwtService, IPasswordHashingService passwordHashingService)
        {
            _userDataService = userDataService;
            _jwtService = jwtService;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<InnerResult<string>> Handle(LoginRequestModel request, CancellationToken cancellationToken)
        {

            var resp = _userDataService.GetByEmail(request.Email);
            if (resp.IsSuccess)
            {
                var user = resp.Value;
                if (_passwordHashingService.Verify(request.Password, user.PasswordHash))
                {
                    var token = _jwtService.Generate(user);
                    return InnerResult<string>.Ok(token);
                }
                else
                {
                    return InnerResult<string>.Fail("Incorrect password!", HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return InnerResult<string>.Fail(resp.Errors.First(), resp.StatusCode);
            }
        }
    }
}
