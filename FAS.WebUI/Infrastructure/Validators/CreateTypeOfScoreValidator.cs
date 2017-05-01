using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateTypeOfScoreViewModelValidator : AbstractValidator<CreateTypeOfScoreViewModel>
    {
        public CreateTypeOfScoreViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
        }
    }
}