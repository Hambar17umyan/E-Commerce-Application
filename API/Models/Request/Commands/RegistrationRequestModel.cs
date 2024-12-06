using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Models.Request.Commands
{
    public class RegistrationRequestModel : IRequest<Result>
    {
        public RegistrationRequestModel(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
