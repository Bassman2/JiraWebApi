namespace JiraWebApi.Service.Model;

/// <summary>
/// Users who has subscribed to an issue.
/// </summary>
internal class SubscriptionsModel
{
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
