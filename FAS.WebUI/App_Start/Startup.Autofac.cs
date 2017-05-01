using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Reflection;
using System.Web.Mvc;
using FAS.DAL.Identity;
using FAS.Domain;
using FAS.WebUI.Infrastructure.Modules;
using FAS.Core;

namespace FAS.WebUI
{
    public partial class Startup
    {
        public void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<DataModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<ValidatorModule>();
            configureOwin(builder, app);
            builder.RegisterType<Core.UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }

        private void configureOwin(ContainerBuilder builder, IAppBuilder appBuilder)
        {

            builder.Register<IAuthenticationManager>((c, p) => c.Resolve<IOwinContext>().Authentication)
                   .InstancePerRequest();
            builder.RegisterType<AppUserStore>()
                   .As<IUserStore<User, Guid>>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<AppRoleStore>()
                   .As<IRoleStore<Role, Guid>>()
                   .InstancePerLifetimeScope();

            builder.Register<AppUserManager>((c, p) =>
            {
                var manager = new AppUserManager(c.Resolve<IUserStore<User, Guid>>());

                manager.UserValidator = new UserValidator<User, Guid>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };

                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                var dataProtectionProvider = appBuilder.GetDataProtectionProvider();

                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
                }

                return manager;
            }).InstancePerLifetimeScope();

            builder.Register<AppRoleManager>((c, p) => new AppRoleManager(c.Resolve<IRoleStore<Role, Guid>>()))
                   .InstancePerLifetimeScope();
        }
    }
}