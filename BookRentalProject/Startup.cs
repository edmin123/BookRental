using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookRentalProject.Startup))]
namespace BookRentalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
