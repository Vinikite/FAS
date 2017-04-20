using FluentValidation;
using System;
using System.Web.Mvc;

namespace FAS.WebUI.Infrastructure
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return DependencyResolver.Current.GetService(validatorType) as IValidator;
        }
    }
}