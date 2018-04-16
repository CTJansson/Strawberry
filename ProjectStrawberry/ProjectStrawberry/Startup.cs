using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectStrawberry.Startup))]
namespace ProjectStrawberry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
