using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FAS.WebUI.Startup))]
namespace FAS.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureAutofac(app);
            ConfigureAutoMapper();
            ConfigureFluentValidation();
        }
    }
}
