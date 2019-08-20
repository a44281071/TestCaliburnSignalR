using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

namespace TestCaliburnMmcIoc
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      GlobalHost.Configuration.DefaultMessageBufferSize = 4096;
      GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = 131072;
      app.UseCors(CorsOptions.AllowAll);

      var config = new HubConfiguration
      {
        Resolver = AppBootstrapper.Resolver,
        EnableDetailedErrors = true
      };

      app.MapSignalR(config);
    }
  }
}