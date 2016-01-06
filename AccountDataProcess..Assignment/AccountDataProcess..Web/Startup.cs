using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountDataProcess.Web.Startup))]
namespace AccountDataProcess.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
