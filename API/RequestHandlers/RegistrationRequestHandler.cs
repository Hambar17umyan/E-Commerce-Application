using API.Data.Repositories;
using API.Models.Domain;
using API.Models.Request;
using API.Services.Control;
using API.Services.DataServices;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.RequestHandlers
{
    public class RegistrationRequestHandler : IRequestHandler<RegistrationRequestModel, Result>
    {
        private UserDataService _userDataService;
        private PasswordHashingService _passwordHashingService;
        public RegistrationRequestHandler(UserDataService userDataService, PasswordHashingService passwordHashingService)
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
