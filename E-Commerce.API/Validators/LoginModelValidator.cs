using E_Commerce.API.Data.Repositories;
using E_Commerce.API.Models.DomainModels;
using E_Commerce.API.Models.RequestModels;
using FluentValidation;
using System.Text.RegularExpressions;

namespace E_Commerce.API.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginModelValidator(UserDataRepository userRepository)
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("The email is empty!");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure);
        }
    }
}
