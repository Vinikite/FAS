using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateTransactionTypeViewModelValidator : AbstractValidator<CreateTransactionTypeViewModel>
    {
        public CreateTransactionTypeViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
        }
    }
}