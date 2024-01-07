using System.Web.Http;
using Khulnasoft.Practices.Unity;
using Unity.WebApi;

namespace Start
{
    public class WebApiConfig
    {
        public static void RegisterWebApi(HttpConfiguration config, IUnityContainer container)
        {
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}