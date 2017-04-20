using FluentValidation;
using FluentValidation.Mvc;
using System.Web.Mvc;
using FAS.WebUI.Infrastructure;
using FAS.WebUI.Infrastructure.Validators;

namespace FAS.WebUI
{
    public partial class Startup
    {
        public void ConfigureFluentValidation()
        {
            var factory = new AutofacValidatorFactory();
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(factory));
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}