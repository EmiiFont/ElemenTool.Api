using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;

[assembly: OwinStartup(typeof(ElementTool.WebApi.Startup))]

namespace ElementTool.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //GlobalConfiguration.Configuration.UseFirebaseStorage(, );

        
            app.UseHangfireDashboard();
        }
    }
}
