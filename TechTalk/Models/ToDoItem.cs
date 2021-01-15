using Newtonsoft.Json;

namespace TechTalk.Models
{
    public class ToDoItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
