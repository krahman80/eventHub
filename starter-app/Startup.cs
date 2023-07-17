using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(starter_app.Startup))]
namespace starter_app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
