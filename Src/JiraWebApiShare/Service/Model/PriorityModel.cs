namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue priority. 
/// </summary>
[DebuggerDisplay("{Id}, {Name}, {Description}")]
internal class PriorityModel
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
    public int Id { get; set; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the JIRA priority.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Url of the icon of the JIRA priority.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri? IconUrl { get; set; }

    /// <summary>
    /// Status color of the JIRA priority.
    /// </summary>
    [JsonPropertyName("statusColor")]
    public string? StatusColor { get; set; }
}
