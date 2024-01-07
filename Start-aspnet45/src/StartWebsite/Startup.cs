using Khulnasoft.ApplicationInsights.Extensibility;
using Khulnasoft.Owin;
using Owin;
using Start;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(Startup))]

//comment
namespace Start
{
	// bellevue comment!!
	// second commit
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            TelemetryConfiguration.Active.InstrumentationKey = WebConfigurationManager.AppSettings["Keys:ApplicationInsights:InstrumentationKey"];

        }
    }
}
