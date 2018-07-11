using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NawazEyeWebProject_NEW_.Startup))]
namespace NawazEyeWebProject_NEW_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
