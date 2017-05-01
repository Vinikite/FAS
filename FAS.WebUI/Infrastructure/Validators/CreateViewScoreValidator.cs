using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateViewScoreViewModelValidator : AbstractValidator<CreateViewScoreViewModel>
    {
        public CreateViewScoreViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
        }
    }
}