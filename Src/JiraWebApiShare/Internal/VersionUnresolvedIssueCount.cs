using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class VersionUnresolvedIssueCount
    {
        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonPropertyName("self")]
        public string Self { get; set; }

        [JsonPropertyName("issuesUnresolvedCount")]
        public int IssuesUnresolvedCount { get; set; }
    }
}
