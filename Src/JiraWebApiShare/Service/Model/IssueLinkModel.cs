namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue link.
/// </summary>
internal sealed class IssueLinkModel
{
    

    /// <summary>
    /// Id of the issue link.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; private set; }

    /// <summary>
    /// Type of the issue link.
    /// </summary>
    [JsonPropertyName("type")]
    public IssueLinkTypeModel? Type { get; set; }

    /// <summary>
    /// Inward issue of the issue link.
    /// </summary>
    [JsonPropertyName("inwardIssue")]
    public Issue? InwardIssue { get; set; }

    /// <summary>
    /// Outward issue of the issue link.
    /// </summary>
    [JsonPropertyName("outwardIssue")]
    public Issue? OutwardIssue { get; set; }

    /// <summary>
    /// Add a comment during issue link creation.
    /// </summary>
    /// <remarks>
    /// Writeonly: Not for getting the comment.
    /// </remarks>
    [JsonPropertyName("comment")]
    public CommentModel? Comment { private get; set; }
}
