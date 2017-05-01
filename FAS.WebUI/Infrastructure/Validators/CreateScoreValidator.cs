using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateScoreViewModelValidator : AbstractValidator<CreateScoreViewModel>
    {
        public CreateScoreViewModelValidator()
        {
            RuleFor(x => x.Balance).NotEmpty();
            RuleFor(x => x.Notation).NotEmpty();
        }
    }
}