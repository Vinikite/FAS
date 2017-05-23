using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateTrasactionViewModelValidator : AbstractValidator<CreateTransactionViewModel>
    {
        public CreateTrasactionViewModelValidator()
        {
            RuleFor(x => x.Comission).NotEmpty();//.Length(10, 64);
            RuleFor(x => x.Notation).NotEmpty();
        }
    }
}