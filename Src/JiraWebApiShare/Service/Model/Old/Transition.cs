namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue transition.
/// </summary>
public sealed class Transition
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
    /// Name of the issue transition.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Result status of this issue transition.
    /// </summary>
    [JsonPropertyName("to")]
    public Status? To { get; set; }

    /// <summary>
    /// Fields which are editable during this transition.
    /// </summary>
    [JsonPropertyName("fields")]
    public Fields? Fields { get; set; }
}
