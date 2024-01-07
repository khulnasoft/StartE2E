using System;
using System.Collections.Generic;
using System.Linq;
using Khulnasoft.AspNet.SignalR;
using Khulnasoft.Practices.Unity;

namespace Start.Utils
{
    public class SignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IUnityContainer inner = Global.UnityContainer.CreateChildContainer();

        public override object GetService(Type serviceType)
        {
            try
            {
				if (serviceType.Name == "NewtownSoft.Serializer")
				{
					return inner.Resolve(serviceType);
				}
				return base.GetService(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return base.GetService(serviceType);
            }
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return inner.ResolveAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}