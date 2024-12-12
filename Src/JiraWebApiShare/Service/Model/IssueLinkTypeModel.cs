namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue link type.
/// </summary>
internal class IssueLinkTypeModel 
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
    /// Name of the inward link.
    /// </summary>
    [JsonPropertyName("inward")]
    public string? Inward { get; set; }

    /// <summary>
    /// Name od the outward link.
    /// </summary>
    [JsonPropertyName("outward")]
    public string? Outward { get; set; }
}
