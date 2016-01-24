using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BulkDataProcess.Web.Startup))]
namespace BulkDataProcess.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
