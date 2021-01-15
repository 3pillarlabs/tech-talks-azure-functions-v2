using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TechTalk.Services;

namespace TechTalk
{
    public class DailyReportFunction
    {
        private readonly IReportService _reportService;

        public DailyReportFunction(IReportService reportService)
        {
            _reportService = reportService;
        }

        [FunctionName("DailyReportFunction")]
        public async Task Run([TimerTrigger("0 0 2 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Daily Report function executed at: {DateTime.UtcNow}");

            try
            {
                await _reportService.CreateReport(DateTime.UtcNow);
                log.LogDebug($"Daily Report function completed at: {DateTime.UtcNow}");
            }
            catch (Exception e)
            {
                log.LogError(e, "Daily Report function error");
            }
        }
    }
}
