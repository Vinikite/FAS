using System.Linq;
using Autofac;
using FAS.Core;

namespace FAS.WebUI.Infrastructure.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterImpInterface(ThisAssembly, "Validator")
                   .InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}