namespace JiraWebApi.Service.Model;

/// <summary>
/// Represents a JIRA icon of a remoteLink object.
/// </summary>
internal class IconModel
{
    /// <summary>
    /// Url of the JIRA icon.
    /// </summary>
    [JsonPropertyName("url16x16")]
    public Uri? Url16x16 { get; set; }

    /// <summary>
    /// Title of the JIRA icon.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Url of the JIRA icon.
    /// </summary>
    [JsonPropertyName("link")]
    public Uri? Link { get; set; }
}
