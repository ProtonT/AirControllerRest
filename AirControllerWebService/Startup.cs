using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AirControllerWebService.Startup))]
namespace AirControllerWebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
