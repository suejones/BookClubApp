using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookClubAppProject.Startup))]
namespace BookClubAppProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
