using Caliburn.Micro;
using System;
using System.Diagnostics;

namespace TestCaliburnMmcIoc
{
  public class ShellViewModel : Screen, IShell, IHandle<string>
  {
    private readonly IEventAggregator eventAggregator;

    public ShellViewModel(IEventAggregator eventAggregator)
    {
      this.eventAggregator = eventAggregator;
    }

    public void MyMethod()
    {
      eventAggregator.PublishOnCurrentThread(DateTime.Now.ToString());
    }

    protected override void OnInitialize()
    {
      eventAggregator.Subscribe(this);
      base.OnInitialize();
    }

    public void Handle(string message)
    {
      Trace.TraceInformation($"handle: {message}");
    }
  }
}