namespace JiraWebApi;

/// <summary>
/// Users who has subscribed to an issue.
/// </summary>
public sealed class Subscriptions
{
    /// <summary>
    /// Initializes a new instance of the Subscriptions class.
    /// </summary>
    private Subscriptions()
    { }

    /// <summary>
    /// Number of the subscriptions.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// Users who has subscribed.
    /// </summary>
    [JsonPropertyName("items")]
    public IEnumerable<User>? Items { get; set; }
}
