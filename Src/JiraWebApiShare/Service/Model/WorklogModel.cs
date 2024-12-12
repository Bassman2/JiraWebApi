namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue worklog. 
/// </summary>
internal class WorklogModel 
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Author of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("author")]
    public UserModel? Author { get; set; }

    /// <summary>
    /// Update author of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("updateAuthor")]
    public UserModel? UpdateAuthor { get; set; }

    /// <summary>
    /// Comment of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Visibility of the JIRA issue worklog.
    /// </summary>
    [JsonPropertyName("visibility")]
    public VisibilityModel? Visibility { get; set; }

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
