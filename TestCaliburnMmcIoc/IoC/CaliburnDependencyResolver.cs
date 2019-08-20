using Caliburn.Micro;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaliburnMmcIoc
{
  public class CaliburnDependencyResolver : DefaultDependencyResolver
  {
    readonly SimpleContainer _container;

    public CaliburnDependencyResolver(SimpleContainer container)
    {
      if (container == null)
        throw new ArgumentNullException("container");

      _container = container;
    }

    public static CaliburnDependencyResolver Current
    {
      get { return GlobalHost.DependencyResolver as CaliburnDependencyResolver; }
    }

    public SimpleContainer LifetimeScope
    {
      get { return _container; }
    }

    public override object GetService(Type serviceType)
    {
      return _container.GetInstance(serviceType, null) ?? base.GetService(serviceType);
    }

    public override IEnumerable<object> GetServices(Type serviceType)
    {
      return _container.GetAllInstances(serviceType).Concat(base.GetServices(serviceType));
    }
  }
}
