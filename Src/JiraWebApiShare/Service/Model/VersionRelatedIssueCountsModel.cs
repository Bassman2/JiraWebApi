namespace JiraWebApi.Service.Model;

internal class VersionRelatedIssueCountsModel
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    [JsonPropertyName("issuesFixedCount")]
    public int IssuesFixedCount { get; set; }

    [JsonPropertyName("issuesAffectedCount")]
    public int IssuesAffectedCount { get; set; }
}
