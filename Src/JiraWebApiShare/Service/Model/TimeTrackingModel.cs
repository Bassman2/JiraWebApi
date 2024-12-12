namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue time tracking.
/// </summary>
internal class TimeTrackingModel
{
    /// <summary>
    /// Time original estimate.
    /// </summary>
    [JsonPropertyName("originalEstimate")]
    public string? OriginalEstimate { get; set; }

    /// <summary>
    /// Time remaining estimate.
    /// </summary>
    [JsonPropertyName("remainingEstimate")]
    public string? RemainingEstimate { get; set; }

    /// <summary>
    /// Time spent.
    /// </summary>
    [JsonPropertyName("timeSpent")]
    public string? TimeSpent { get; set; }

    /// <summary>
    /// Time original estimate in seconds.
    /// </summary>
    [JsonPropertyName("originalEstimateSeconds")]
    public long OriginalEstimateSeconds { get; set; }

    /// <summary>
    /// Time remaining estimate in seconds.
    /// </summary>
    [JsonPropertyName("remainingEstimateSeconds")]
    public long RemainingEstimateSeconds { get; set; }

    /// <summary>
    /// Time spent in seconds.
    /// </summary>
    [JsonPropertyName("timeSpentSeconds")]
    public long TimeSpentSeconds { get; set; }
}
