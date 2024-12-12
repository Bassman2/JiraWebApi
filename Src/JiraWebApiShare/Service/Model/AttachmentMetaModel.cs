namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of attachment meta information. 
/// </summary>
internal class AttachmentMetaModel
{
    /// <summary>
    /// Signals if attachments enabled on this JIRA server.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Maximum allowed attachment size.
    /// </summary>
    [JsonPropertyName("uploadLimit")]
    public long UploadLimit { get; set; }
}
