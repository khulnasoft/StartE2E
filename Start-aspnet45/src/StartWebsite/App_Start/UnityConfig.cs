using Khulnasoft.Practices.Unity;
using Start.Models;
using Start.ProductSearch;
using Start.Recommendations;
using Start.Utils;

namespace Start
{
    public class UnityConfig
    {
        public static UnityContainer BuildContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IStartContext, StartContext>();
            container.RegisterType<IOrdersQuery, OrdersQuery>();
            container.RegisterType<IRaincheckQuery, RaincheckQuery>();
            container.RegisterType<IRecommendationEngine, AzureMLFrequentlyBoughtTogetherRecommendationEngine>();
            container.RegisterType<ITelemetryProvider, TelemetryProvider>();
            container.RegisterType<IProductSearch, StringContainsProductSearch>();
            container.RegisterType<IShippingTaxCalculator, DefaultShippingTaxCalculator>();

			container.RegisterInstance<IHttpClient>(container.Resolve<HttpClientWrapper>());

            return container;
        }
    }
}