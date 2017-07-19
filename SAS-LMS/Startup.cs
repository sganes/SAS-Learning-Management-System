using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SAS_LMS.Startup))]
namespace SAS_LMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
