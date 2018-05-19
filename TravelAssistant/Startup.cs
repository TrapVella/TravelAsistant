using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelAssistant.Startup))]
namespace TravelAssistant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
