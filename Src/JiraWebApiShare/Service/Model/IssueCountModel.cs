namespace JiraWebApi.Service.Model;

internal class IssueCountModel
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("issueCount")]
    public int IssueCount { get; set; }
}
