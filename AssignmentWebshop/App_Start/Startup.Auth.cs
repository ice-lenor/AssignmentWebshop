using Microsoft.AspNet.Identity;
using Owin;
using AssignmentWebshop.Models;

namespace AssignmentWebshop
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
        }
    }
}