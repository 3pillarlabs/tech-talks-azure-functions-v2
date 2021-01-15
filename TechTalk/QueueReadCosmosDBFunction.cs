using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using TechTalk.Models;

namespace TechTalk
{
    public static class QueueReadCosmosDBFunction
    {
        [FunctionName("QueueReadCosmosDBFunction")]
        [return: Queue("todoqueue")]
        public static ToDoItem Run(
            [QueueTrigger("todoqueueforlookup")] ToDoItemLookup toDoItemLookup,
            [CosmosDB(
                databaseName: "ToDoItems",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{ToDoItemId}",
                PartitionKey = "{ToDoItemPartitionKeyValue}")]ToDoItem toDoItem,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger Id={toDoItemLookup?.ToDoItemId} Key={toDoItemLookup?.ToDoItemPartitionKeyValue}");

            return toDoItem;
        }
    }
}
