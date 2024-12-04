namespace JiraWebApi.Service.Model;

internal class VersionUnresolvedIssueCount
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("issuesUnresolvedCount")]
    public int IssuesUnresolvedCount { get; set; }
}
