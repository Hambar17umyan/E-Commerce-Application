using E_Commerce.API.Data.Repositories;
using E_Commerce.API.Models.RequestModels;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace E_Commerce.API.Validators
{
    public class RegistrationModelValidator : AbstractValidator<RegistrationRequestModel>
    {
        public RegistrationModelValidator(UserDataRepository userDataRepository)
        {
            Regex nameVal = new(@"([A-Z][a-z]*)([\\s\\\'-][A-Z][a-z]*)*");
            Regex passwordVal = new(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(reg => reg.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("First name is empty!")
                .Must(name => nameVal.Match(name).Success)
                .WithMessage("First name is in a wrong format!");

            RuleFor(reg => reg.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Last name is empty!")
                .Must(name => nameVal.Match(name).Success)
                .WithMessage("Last name is in a wrong format!");

            RuleFor(reg => reg.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email address is not valid!")
                .Must(email =>
                 userDataRepository.GetAllAsQueryable().FirstOrDefault(u => u.Email == email) == null)
                .WithMessage("There is already a user with that email address!");

            RuleFor(reg => reg.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(pass => passwordVal.Match(pass).Success)
                .WithMessage("Password is in a wrong format! It should: \r\n\r\n1) Have minimum 8 characters in length. Adjust it by modifying {8,}\r\n\r\n2) Have at least one uppercase English letter. You can remove this condition by removing (?=.*?[A-Z])\r\n\r\n3) Have at least one lowercase English letter.  You can remove this condition by removing (?=.*?[a-z])\r\n\r\n4) Have at least one digit. You can remove this condition by removing (?=.*?[0-9])) \r\n\r\n5) Have at least one special character,  You can remove this condition by removing (?=.*?[#?!@$%^&*-])");
        }

    }
}
