using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class WorklogGetResult
    {
        [JsonPropertyName("startAt")]
        public int StartAt { get; set; }

        [JsonPropertyName("maxResults")]
        public int MaxResults { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("worklogs")]
        public IEnumerable<Worklog> Worklogs { get; set; }
    }
}
