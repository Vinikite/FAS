using System.Linq;
using Autofac;

namespace FAS.WebUI.Infrastructure.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterImpInterface(ThisAssembly, "Validator")
                   .InstancePerLifetimeScope();
        }
    }
}