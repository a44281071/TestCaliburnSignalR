namespace TestCaliburnMmcIoc
{
  using System;
  using System.Collections.Generic;
  using Caliburn.Micro;
  using Microsoft.AspNet.SignalR;
  using Microsoft.Owin.Hosting;

  public class AppBootstrapper : BootstrapperBase
  {
    public AppBootstrapper()
    {
      Initialize();
    }

    private SimpleContainer container;
    private IDisposable _Server;

    public static IDependencyResolver Resolver { get; private set; }

    protected override void Configure()
    {
      container = new SimpleContainer();

      container.Singleton<IWindowManager, WindowManager>();
      container.Singleton<IEventAggregator, EventAggregator>();
      container.PerRequest<IShell, ShellViewModel>();
      Resolver = new CaliburnDependencyResolver(container);
    }

    protected override object GetInstance(Type service, string key)
    {
      return container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      return container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
      container.BuildUp(instance);
    }

    protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
    {
      // need ADMIN or add urlacl.
      _Server = WebApp.Start("http://*:18685");
      DisplayRootViewFor<IShell>();
    }

    protected override void OnExit(object sender, EventArgs e)
    {
      _Server.Dispose();
      base.OnExit(sender, e);
    }
  }
}