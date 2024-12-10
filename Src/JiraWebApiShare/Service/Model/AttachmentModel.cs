namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue attachment. 
/// </summary>
internal class AttachmentModel 
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
    /// File name of the JIRA attachment.
    /// </summary>
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    /// <summary>
    /// Author of the JIRA attachment.
    /// </summary>
    [JsonPropertyName("author")]
    public User? Author { get; set; }

    /// <summary>
    /// Creation data of the JIRA attachment.
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    /// <summary>
    /// Size of the JIRA attachment.
    /// </summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>
    /// Mime type of the JIRA attachment.
    /// </summary>
    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }

    /// <summary>
    /// Url of the content of the JIRA attachment.
    /// </summary>
    [JsonPropertyName("content")]
    public Uri? Content { get; set; }

    /// <summary>
    /// Url of the thumbnail of the JIRA attachment.
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public Uri? Thumbnail { get; set; }
}
