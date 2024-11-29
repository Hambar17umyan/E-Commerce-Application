using API.Data.Repositories.Concrete;
using API.Models.Request;
using FluentValidation;

namespace API.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The email is empty!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("The password is empty!");
        }
    }
}
