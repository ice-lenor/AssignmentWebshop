using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssignmentWebshop.Startup))]
namespace AssignmentWebshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
