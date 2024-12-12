namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue comment. 
/// </summary>
internal class CommentModel 
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
    /// Author of the JIRA comment.
    /// </summary>
    [JsonPropertyName("author")]
    public User? Author { get; set; }

    /// <summary>
    /// Body of the JIRA comment.
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// Update author of the JIRA comment.
    /// </summary>
    [JsonPropertyName("updateAuthor")]
    public User? UpdateAuthor { get; set; }

    /// <summary>
    /// Creation data of the JIRA comment.
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    /// <summary>
    /// Update data of the JIRA comment.
    /// </summary>
    [JsonPropertyName("updated")]
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Visibility of the JIRA comment.
    /// </summary>
    [JsonPropertyName("visibility")]
    public VisibilityModel? Visibility { get; set; }
}
