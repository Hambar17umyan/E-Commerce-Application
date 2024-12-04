using API.Models.Request;
using API.Services.Concrete.Control;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;

namespace API.RequestHandlers
{
    public class LoginRequestHandler : IRequestHandler<LoginRequestModel, Result<string>>
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

        public async Task<Result<string>> Handle(LoginRequestModel request, CancellationToken cancellationToken)
        {

            var resp = _userDataService.GetByEmail(request.Email);
            if(resp.IsSuccess)
            {
                var user = resp.Value;
                if (_passwordHashingService.Verify(request.Password, user.PasswordHash))
                {
                    var token = _jwtService.Generate(user);
                    return Result.Ok(token);
                }
                else
                {
                    return Result.Fail("Incorrect password!");
                }
            }
            else
            {
                return Result.Fail(resp.Errors.First());
            }
        }
    }
}
