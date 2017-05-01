using System.Data.Entity;
using Autofac;
using FAS.DAL;
using FAS.DAL.Repository;
using FAS.Core;

namespace FAS.WebUI.Infrastructure.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().As<DbContext>().InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(AppRepository<>))
                   .As(typeof(IAppRepository<>))
                   .InstancePerLifetimeScope();
        }
    }
}