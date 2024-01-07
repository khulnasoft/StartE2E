using Khulnasoft.ApplicationInsights.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start.Utils
{
    public class StartTelemetryInitializer
    {
        string appVersion = GetApplicationVersion();

        private static string GetApplicationVersion()
        {
            return typeof(StartTelemetryInitializer).Assembly.GetName().Version.ToString();
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Component.Version = appVersion;
        }
    }
}