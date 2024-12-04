namespace JiraWebApi.Service.Model;

internal class VersionRelatedIssueCounts
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("issuesFixedCount")]
    public int IssuesFixedCount { get; set; }

    [JsonPropertyName("issuesAffectedCount")]
    public int IssuesAffectedCount { get; set; }
}
