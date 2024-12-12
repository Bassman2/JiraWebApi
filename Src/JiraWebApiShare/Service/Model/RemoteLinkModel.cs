namespace JiraWebApi.Service.Model;

/// <summary>
/// Rrepresentation of a JIRA issue remote link. 
/// </summary>
internal class RemoteLinkModel
{

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    /// <summary>
    /// Global Id of the JIRA issue global link.
    /// </summary>
    [JsonPropertyName("globalId")]
    public string? GlobalId { get; set; }
    
    /// <summary>
    /// Application of the remote link.
    /// </summary>
    [JsonPropertyName("application")]
    public ApplicationModel? Application { get; set; }

    /// <summary>
    /// Relationship of the JIRA issue global link.
    /// </summary>
    [JsonPropertyName("relationship")]
    public string? Relationship { get; set; }

    /// <summary>
    /// Object of the remote link.
    /// </summary>
    [JsonPropertyName("object")]
    public Object? Object { get; set; }
}
