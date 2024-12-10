namespace JiraWebApi;

/// <summary>
/// Signals to whom the notification should be send.
/// </summary>
public sealed class NotifyTo
{
    /// <summary>
    /// Initializes a new instance of the NotifyTo class.
    /// </summary>
    public NotifyTo()
    { }

    /// <summary>
    /// Signals if the notification should be send to the reporter of the issue.
    /// </summary>
    [JsonPropertyName("reporter")]
    public bool Reporter { get; set; }

    /// <summary>
    /// Signals if the notification should be send to the assignee of the issue.
    /// </summary>
    [JsonPropertyName("assignee")]
    public bool Assignee { get; set; }

    /// <summary>
    /// Signals if the notification should be send to all watchers of the issue.
    /// </summary>
    [JsonPropertyName("watchers")]
    public bool Watchers { get; set; }

    /// <summary>
    /// Signals if the notification should be send to all voters of the issue.
    /// </summary>
    [JsonPropertyName("voters")]
    public bool Voters { get; set; }

    /*
    /// <summary>
    /// List of users to which the notification should be send.
    /// </summary>
    [JsonPropertyName("users")]
    public IEnumerable<User> Users { get; set; }

    /// <summary>
    /// List of groups to which the notification should be send.
    /// </summary>
    [JsonPropertyName("groups")]
    public IEnumerable<Group> Groups { get; set; }
    */
}
