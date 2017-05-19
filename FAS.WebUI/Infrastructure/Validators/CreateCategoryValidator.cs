using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateCategoryViewModelValidator : AbstractValidator<CreateCategoryViewModel>
    {
        public CreateCategoryViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
        }
    }
}