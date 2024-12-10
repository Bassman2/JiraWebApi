namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue priority. 
/// </summary>
[DebuggerDisplay("{Id}, {Name}, {Description}")]
public sealed class Priority 
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
    /// Name of the JIRA item.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the JIRA priority.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; private set; }

    /// <summary>
    /// Url of the icon of the JIRA priority.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri? IconUrl { get; private set; }

    /// <summary>
    /// Status color of the JIRA priority.
    /// </summary>
    [JsonPropertyName("statusColor")]
    public string? StatusColor { get; private set; }
}
