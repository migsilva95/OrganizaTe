using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrganizaTe.Startup))]
namespace OrganizaTe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
