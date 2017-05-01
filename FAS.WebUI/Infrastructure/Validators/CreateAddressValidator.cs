using FluentValidation;
using FAS.WebUI.Models;

namespace FAS.WebUI.Infrastructure.Validators
{
    public class CreateAddressViewModelValidator : AbstractValidator<CreateAddressViewModel>
    {
        public CreateAddressViewModelValidator()
        {
            RuleFor(x => x.Country).NotEmpty().Length(10, 64);
            RuleFor(x => x.City).NotEmpty().Length(10, 64);
            RuleFor(x => x.Street).NotEmpty().Length(10, 64);
            RuleFor(x => x.House).NotEmpty().Length(10, 64);
            RuleFor(x => x.Flat).NotEmpty().Length(10, 64);//RuleFor(x => x.Flat).NotEmpty().GreaterThan(0).LessThan(decimal.MaxValue);
        }
    }
}