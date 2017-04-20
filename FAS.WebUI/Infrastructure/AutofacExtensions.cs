using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using System;
using System.Linq;
using System.Reflection;

namespace FAS.WebUI.Infrastructure
{
    public static class AutofacExtensions
    {
        public static IRegistrationBuilder<Object, ScanningActivatorData, DynamicRegistrationStyle> RegisterImpInterface(
            this ContainerBuilder builder, Assembly assembly, string endsWith)
        {
            return builder.RegisterAssemblyTypes(assembly)
                          .Where(obj => obj.Name.EndsWith(endsWith))
                          .AsImplementedInterfaces();
        }
    }
}