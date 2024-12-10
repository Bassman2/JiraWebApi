namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of attachment meta information. 
/// </summary>
internal sealed class AttachmentMetaModel
{
    

    /// <summary>
    /// Signals if attachments enabled on this JIRA server.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool IsEnabled { get; private set; }

    /// <summary>
    /// Maximum allowed attachment size.
    /// </summary>
    [JsonPropertyName("uploadLimit")]
    public long UploadLimit { get; private set; }
}
