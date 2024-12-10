namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue comment. 
/// </summary>
public sealed class Comment : ElementModel
{
    /// <summary>
    /// Initializes a new instance of the Comment class.
    /// </summary>
    public Comment()
    { }

    /// <summary>
    /// Author of the JIRA comment.
    /// </summary>
    [JsonPropertyName("author")]
    public User? Author { get; private set; }

    /// <summary>
    /// Body of the JIRA comment.
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// Update author of the JIRA comment.
    /// </summary>
    [JsonPropertyName("updateAuthor")]
    public User? UpdateAuthor { get; private set; }

    /// <summary>
    /// Creation data of the JIRA comment.
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime? Created { get; internal set; }

    /// <summary>
    /// Update data of the JIRA comment.
    /// </summary>
    [JsonPropertyName("updated")]
    public DateTime? Updated { get; internal set; }

    /// <summary>
    /// Visibility of the JIRA comment.
    /// </summary>
    [JsonPropertyName("visibility")]
    public Visibility? Visibility { get; set; }
}
