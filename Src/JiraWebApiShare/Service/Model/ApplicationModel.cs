namespace JiraWebApi.Service.Model;

/// <summary>
/// Represent an Application for remote links. 
/// </summary>
internal class ApplicationModel
{
    /// <summary>
    /// Type of the application.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Name of the application.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
