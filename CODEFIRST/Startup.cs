using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CODEFIRST.Startup))]
namespace CODEFIRST
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
