using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class AssignPutRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
