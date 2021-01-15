using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechTalk.Services;

[assembly: FunctionsStartup(typeof(TechTalk.Startup))]

namespace TechTalk
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<IReportService, ReportService>();
            builder.Services.AddScoped<IReportService, ReportService>();

            if (Environment.GetEnvironmentVariable("Environment") == "Local")
            {
                builder.Services.AddLogging(logging =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                });
            }
            else
            {
                builder.Services.AddLogging();
            }
        }
    }
}
