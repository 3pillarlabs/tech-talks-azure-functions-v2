using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace TechTalk
{
    public static class OrchestratorFunction
    {
        [FunctionName("OrchestratorFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            dynamic input = context.GetInput<object>();
            var city = input?.City?.ToString();

            var outputs = new List<string>
            {
                await context.CallActivityAsync<string>("OrchestratorFunction_Hello", "Tokyo"),
                await context.CallActivityAsync<string>("OrchestratorFunction_Hello", "Seattle"),
                await context.CallActivityAsync<string>("OrchestratorFunction_Hello", "London"),
            };

            if (!string.IsNullOrWhiteSpace(city))
                outputs.Add(await context.CallActivityAsync<string>("OrchestratorFunction_Hello", city));

            return outputs;
        }

        [FunctionName("OrchestratorFunction_Hello")]
        //public static string SayHello([ActivityTrigger] string name, ILogger log)
        public static string SayHello([ActivityTrigger] IDurableActivityContext ctx, ILogger log)
        {
            var name = ctx.GetInput<string>();
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }
    }
}