using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class ComponentRelatedIssueCounts
    {
        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonPropertyName("self")]
        public string Self { get; set; }

        [JsonPropertyName("issueCount")]
        public int IssueCount { get; set; }
    }
}
