namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue worklog. 
/// </summary>
public sealed class Worklog 
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Author of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("author")]
    public User? Author { get; set; }

    /// <summary>
    /// Update author of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("updateAuthor")]
    public User? UpdateAuthor { get; set; }

    /// <summary>
    /// Comment of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Visibility of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("visibility")]
    public Visibility? Visibility { get; set; }

    /// <summary>
    /// Start date of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("started")]
    public DateTime? Started { get; set; }

    /// <summary>
    /// Time spend of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("timeSpent")]
    public string? TimeSpent { get; set; }

    /// <summary>
    /// Time spend in seconds of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("timeSpentSeconds")]
    public int TimeSpentSeconds { get; set; }
}
