using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateStatusScoreViewModelValidator : AbstractValidator<CreateStatusScoreViewModel>
    {
        public CreateStatusScoreViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
        }
    }
}