namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue link.
/// </summary>
public sealed class IssueLink
{
    /// <summary>
    /// Initializes a new instance of the Link class.
    /// </summary>
    public IssueLink()
    { }

    /// <summary>
    /// Id of the issue link.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; private set; }

    /// <summary>
    /// Type of the issue link.
    /// </summary>
    [JsonPropertyName("type")]
    public IssueLinkType? Type { get; set; }

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
    public Comment? Comment { private get; set; }
}
