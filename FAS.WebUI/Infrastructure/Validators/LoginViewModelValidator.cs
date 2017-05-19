using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Length(8, 64);
        }
    }
}