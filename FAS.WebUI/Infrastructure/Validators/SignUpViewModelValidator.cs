//using FluentValidation;
//using FAS.WebUI.Models;

//namespace FAS.WebUI.Infrastructure.Validators
//{
//    public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
//    {
//        public SignUpViewModelValidator()
//        {
//            RuleFor(x => x.Login).NotEmpty().EmailAddress();
//            RuleFor(x => x.Password).NotEmpty().Length(8, 64);
//            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.ConfirmPassword);
//        }
//    }
//}