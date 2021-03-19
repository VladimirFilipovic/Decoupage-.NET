using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KC_Decoupage.Startup))]
namespace KC_Decoupage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
