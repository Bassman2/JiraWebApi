namespace JiraWebApi.Service.Model;

/// <summary>
/// Object of a remote link.
/// </summary>
internal class ObjectModel
{
    /// <summary>
    /// Url of the remote link object.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    /// <summary>
    /// Title of the remote link object.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Summary of the remote link object.
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>
    /// Icon of the remote link object.
    /// </summary>
    [JsonPropertyName("icon")]
    public IconModel? Icon { get; set; }

    // TODO
    //[JsonPropertyName("status")]
    //public Status Status { get; set; }
}
