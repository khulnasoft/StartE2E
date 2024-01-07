using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Khulnasoft.Practices.Unity;
using Start.Utils;
using Unity.Mvc4;

namespace Start
{
    public class Global : HttpApplication
    {
        internal static IUnityContainer UnityContainer;

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            Database.SetInitializer(new StartDbInitializer());

            UnityContainer = UnityConfig.BuildContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityContainer));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            WebApiConfig.RegisterWebApi(GlobalConfiguration.Configuration, UnityContainer);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}