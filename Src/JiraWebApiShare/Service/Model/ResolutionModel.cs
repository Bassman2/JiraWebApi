namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue resolution. 
/// </summary>
[DebuggerDisplay("{Id}, {Name}, {Description}")]
internal class ResolutionModel
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
    /// Description of the JIRA resolution.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; private set; }
}
