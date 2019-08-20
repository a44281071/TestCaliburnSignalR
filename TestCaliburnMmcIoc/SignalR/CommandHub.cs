using Caliburn.Micro;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaliburnMmcIoc.SignalR
{
  public class CommandHub : Hub
  {
    public CommandHub(IEventAggregator eventAggregator)
    {
      _EventAggregator = eventAggregator;
    }

    private readonly IEventAggregator _EventAggregator;

    public override Task OnConnected()
    {
      Trace.TraceInformation("CommandHub.OnConnected");
      return base.OnConnected();
    }
  }
}
