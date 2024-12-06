using API.Data.Repositories.Concrete;
using API.Data.Repositories.Interfaces;
using API.Models.Request.Commands;
using API.Services.Interfaces.DataServices;
using FluentValidation;
using System.Text.RegularExpressions;

namespace API.Validators
{
    public class RegistrationModelValidator : AbstractValidator<RegistrationRequestModel>
    {
        public RegistrationModelValidator(IUserDataService userDataService)
        {
            Regex nameVal = new(@"([A-Z][a-z]*)([\\s\\\'-][A-Z][a-z]*)*");
            Regex passwordVal = new(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            
            RuleFor(reg => reg.FirstName)
                .NotEmpty()
                .WithMessage("First name is empty!")
                .Must(name => nameVal.Match(name).Success)
                .WithMessage("First name is in a wrong format!");

            RuleFor(reg => reg.LastName)
                .NotEmpty()
                .WithMessage("Last name is empty!")
                .Must(name => nameVal.Match(name).Success)
                .WithMessage("Last name is in a wrong format!");

            RuleFor(reg => reg.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email address is not valid!")
                .Must(email =>
                 userDataService.GetAll().Value.FirstOrDefault(u => u.Email == email) == null)
                .WithMessage("There is already a user with that email address!");

            RuleFor(reg => reg.Password)
                .NotEmpty()
                .Must(pass => passwordVal.Match(pass).Success)
                .WithMessage("Password is in a wrong format! It should: \r\n\r\n1) Have minimum 8 characters in length. Adjust it by modifying {8,}\r\n\r\n2) Have at least one uppercase English letter. You can remove this condition by removing (?=.*?[A-Z])\r\n\r\n3) Have at least one lowercase English letter.  You can remove this condition by removing (?=.*?[a-z])\r\n\r\n4) Have at least one digit. You can remove this condition by removing (?=.*?[0-9])) \r\n\r\n5) Have at least one special character,  You can remove this condition by removing (?=.*?[#?!@$%^&*-])");
        }

    }
}
