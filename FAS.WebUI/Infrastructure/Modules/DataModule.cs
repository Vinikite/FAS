using System.Data.Entity;
using Autofac;
using FAS.DAL;
using FAS.DAL.Repository;

namespace FAS.WebUI.Infrastructure.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterImpInterface(typeof(AuthorRepository).Assembly, "Repository")
                   .InstancePerLifetimeScope();
        }
    }
}