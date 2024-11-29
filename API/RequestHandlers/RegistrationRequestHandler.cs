using API.Data.Repositories;
using API.Models.Domain;
using API.Models.Request;
using API.Services.Concrete.Control;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.RequestHandlers
{
    public class RegistrationRequestHandler : IRequestHandler<RegistrationRequestModel, Result>
    {
        private IUserDataService _userDataService;
        private IPasswordHashingService _passwordHashingService;
        public RegistrationRequestHandler(IUserDataService userDataService, IPasswordHashingService passwordHashingService)
        {
            _userDataService = userDataService;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<Result> Handle(RegistrationRequestModel request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = _passwordHashingService.Hash(request.Password)
            };
            return await _userDataService.AddAsync(user);
        }
    }
}
