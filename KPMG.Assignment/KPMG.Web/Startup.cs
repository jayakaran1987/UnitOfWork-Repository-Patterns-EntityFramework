using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KPMG.Web.Startup))]
namespace KPMG.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
