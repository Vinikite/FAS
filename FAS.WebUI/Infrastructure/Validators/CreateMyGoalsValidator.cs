using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateMyGoalsViewModelValidator : AbstractValidator<CreateMyGoalsViewModel>
    {
        public CreateMyGoalsViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}