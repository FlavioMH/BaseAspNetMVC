using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaseAspNetMvc.Web.Startup))]
namespace BaseAspNetMvc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
