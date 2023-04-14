using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(W2022A6NNB.Startup))]

namespace W2022A6NNB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
