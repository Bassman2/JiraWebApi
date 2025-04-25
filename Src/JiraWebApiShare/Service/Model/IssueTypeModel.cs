namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue type. 
/// </summary>
internal class IssueTypeModel 
{
    /// <summary>
    /// Initializes a new instance of the IssueType class.
    /// </summary>

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    
    /// <summary>
    /// Description of the JIRA issue type.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Url of the issue type icon.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri? IconUrl { get; set; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Signals if the issue type is a subtask issue type.
    /// </summary>
    [JsonPropertyName("subtask")]
    public bool? IsSubtask { get; set; }

    /// <summary>
    /// Signals if the issue type is a subtask issue type.
    /// </summary>
    [JsonPropertyName("avatarId")]
    public int? AvatarId { get; set; }

    // /// <summary>
    // /// Name of the classes which should be expanded.
    // /// </summary>
    //[JsonPropertyName("expand")]
    //public string? Expand { get; set; }

    // /// <summary>
    // /// Fields which are editable for this issue type. Used by meta information.
    // /// </summary>
    // /// <remarks>Only filled by <see cref="Jira.GetCreateMetaAsync">GetCreateMetaAsync</see> and <see cref="Jira.GetEditMetaAsync">GetEditMetaAsync</see>.</remarks>
    //[JsonPropertyName("fields")]
    //public Fields? Fields { get; set; }
}
