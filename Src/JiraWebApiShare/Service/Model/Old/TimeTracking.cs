namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue time tracking.
/// </summary>
public sealed class TimeTracking
{
    /// <summary>
    /// Initializes a new instance of the TimeTracking class.
    /// </summary>
    public TimeTracking()
    { }

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
    public long OriginalEstimateSeconds { get; private set; }

    /// <summary>
    /// Time remaining estimate in seconds.
    /// </summary>
    [JsonPropertyName("remainingEstimateSeconds")]
    public long RemainingEstimateSeconds { get; private set; }

    /// <summary>
    /// Time spent in seconds.
    /// </summary>
    [JsonPropertyName("timeSpentSeconds")]
    public long TimeSpentSeconds { get; private set; }
}
