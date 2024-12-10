namespace JiraWebApi.Service.Model;

internal class VersionUnresolvedIssueCountModel
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("issuesUnresolvedCount")]
    public int IssuesUnresolvedCount { get; set; }
}
