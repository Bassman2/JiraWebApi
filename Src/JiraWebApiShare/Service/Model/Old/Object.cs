namespace JiraWebApi;

/// <summary>
/// Object of a remote link.
/// </summary>
public sealed class Object
{
    /// <summary>
    /// Initializes a new instance of the Object class.
    /// </summary>
    public Object()
    { }

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
    public Icon? Icon { get; set; }

    // TODO
    //[JsonPropertyName("status")]
    //public Status Status { get; set; }
}
