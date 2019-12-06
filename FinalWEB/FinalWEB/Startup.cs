using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalWEB.Startup))]
namespace FinalWEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
