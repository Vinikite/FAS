using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateBookViewModelValidator : AbstractValidator<CreateBookViewModel>
    {
        public CreateBookViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0).LessThan(decimal.MaxValue);
        }
    }
}