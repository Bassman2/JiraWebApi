using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class TransitionPostReq
    {
        [JsonPropertyName("update")]
        public object Update { get; set; }

        [JsonPropertyName("fields")]
        public Fields Fields { get; set; }

        [JsonPropertyName("transition")]
        public Transition Transition { get; set; }
    }
}
