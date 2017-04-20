using System.Linq;
using Autofac;
using FAS.Core;

namespace FAS.WebUI.Infrastructure.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterImpInterface(typeof(AuthorService).Assembly, "Service")
                   .InstancePerLifetimeScope();
        }
    }
}