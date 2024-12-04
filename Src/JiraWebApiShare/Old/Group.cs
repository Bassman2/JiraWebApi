namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA group.
/// </summary>
public sealed class Group
{
    /// <summary>
    /// Initializes a new instance of the Group class.
    /// </summary>
    public Group()
    { }

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    /// <summary>
    /// Name of the JIRA group.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Users of the JIRA group.
    /// </summary>
    [JsonPropertyName("users")]
    public Users? Users { get; set; }

    /// <summary>
    /// Names of the expanded fields.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; set; }
}
