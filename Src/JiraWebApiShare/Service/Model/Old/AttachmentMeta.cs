namespace JiraWebApi;

/// <summary>
/// Representation of attachment meta information. 
/// </summary>
internal sealed class AttachmentMeta
{
    /// <summary>
    /// Initializes a new instance of the AttachmentMeta class.
    /// </summary>
    private AttachmentMeta()
    { }

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
