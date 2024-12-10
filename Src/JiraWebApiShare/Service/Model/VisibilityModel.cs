namespace JiraWebApi.Service.Model;

/// <summary>
/// Rrepresentation of the visibility of a JIRA item. 
/// </summary>
internal class VisibilityModel
{
    

    /// <summary>
    /// Type of the JIRA visibility.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Value of the JIRA visibility.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
