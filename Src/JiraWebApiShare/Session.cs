using System.Text.Json.Serialization;

namespace JiraWebApi
{
    public class Session
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
