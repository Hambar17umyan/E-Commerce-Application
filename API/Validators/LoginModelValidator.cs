using API.Data.Repositories;
using API.Models.Request;
using FluentValidation;

namespace API.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginModelValidator(UserDataRepository userRepository)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The email is empty!");

            RuleFor(x => x.Password);
        }
    }
}
