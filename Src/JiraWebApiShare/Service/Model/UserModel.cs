namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA user. 
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class UserModel
{
    /// <summary>
    /// Url of the REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Key of the REST item.
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Name of the JIRA user.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// E-mail address of the JIRA user.
    /// </summary>
    [JsonPropertyName("emailAddress")]
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Avatar URLs of the JIRA user.
    /// </summary>
    [JsonPropertyName("avatarUrls")]
    public AvatarUrlsModel? AvatarUrls { get; set; }

    /// <summary>
    /// Display name of the JIRA user.
    /// </summary>
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    /// <summary>
    /// Signals if the JIRA user is actve.
    /// </summary>
    [JsonPropertyName("active")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Signals if the JIRA user is deleted.
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Time zone of the JIRA user.
    /// </summary>
    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; set; }

    /// <summary>
    /// Time zone of the JIRA user.
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; set; }
}
