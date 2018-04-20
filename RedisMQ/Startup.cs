using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedisMQ.Startup))]
namespace RedisMQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
