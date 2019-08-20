namespace TestCaliburnMmcIoc
{
  using System;
  using System.Collections.Generic;
  using Caliburn.Micro;
  using Microsoft.AspNet.SignalR;
  using Microsoft.Owin.Hosting;
  using Ninject;
  using TestCaliburnMmcIoc.SignalR;

  public class AppNinjectBootstrapper : BootstrapperBase
  {
    public AppNinjectBootstrapper()
    {
      Initialize();
    }

    private IDisposable _Server;
    private IKernel container = new StandardKernel();

    public static IDependencyResolver Resolver { get; private set; }

    protected override void Configure()
    {
      // services
      container.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
      container.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
      container.Bind<IShell>().To<ShellViewModel>();
      container.Bind<CommandHub>();

      Resolver = new NinjectDependencyResolver(container);
    }

    protected override object GetInstance(Type service, string key)
    {
      return String.IsNullOrEmpty(key)
           ? container.Get(service)
           : container.Get(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      return container.GetAll(service);
    }

    protected override void BuildUp(object instance)
    {
      container.Inject(instance);
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
      container.Dispose();
      base.OnExit(sender, e);
    }
  }
}