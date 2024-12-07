using API.Data.Repositories;
using API.Models.Control.ResultModels;
using API.Models.Domain;
using API.Models.Domain.Concrete;
using API.Models.Request.Commands;
using API.Services.Concrete.Control;
using API.Services.Concrete.DataServices;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.RequestHandlers.CommandHandlers
{
    public class RegistrationRequestHandler : IRequestHandler<RegistrationRequestModel, InnerResult>
    {
        private IUserDataService _userDataService;
        private IPasswordHashingService _passwordHashingService;
        private IRoleDataService _roleDataService;
        public RegistrationRequestHandler(IUserDataService userDataService, IPasswordHashingService passwordHashingService, IRoleDataService roleDataService)
        {
            _userDataService = userDataService;
            _passwordHashingService = passwordHashingService;
            _roleDataService = roleDataService;
        }

        public async Task<InnerResult> Handle(RegistrationRequestModel request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = _passwordHashingService.Hash(request.Password),
                Roles = new List<Role>()
                {
                    _roleDataService.GetCustomer()
                },
                Cart = new Cart(),
                IsActive = true,
                Orders = new List<Order>()
            };
            return await _userDataService.AddAsync(user);
        }
    }
}
