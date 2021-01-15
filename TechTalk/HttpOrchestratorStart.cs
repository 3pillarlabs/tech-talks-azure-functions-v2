using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace TechTalk
{
    public static class HttpOrchestratorStart
    {
        //When debugging locally call the function with a Postman GET: http://localhost:7071/api/start/OrchestratorFunction/Miami
        [FunctionName("HttpOrchestratorStart")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "start/{functionName}/{value}")] HttpRequestMessage req,
            [DurableClient] IDurableClient starter,
            string functionName,
            string value,
            ILogger log)
        {
            var instanceId = await starter.StartNewAsync(functionName, new {City = value});

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return await starter.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId);
            //return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}

