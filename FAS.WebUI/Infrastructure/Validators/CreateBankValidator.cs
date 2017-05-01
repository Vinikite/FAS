using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateBankViewModelValidator : AbstractValidator<CreateBankViewModel>
    {
        public CreateBankViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(10, 64);
        }
    }
}