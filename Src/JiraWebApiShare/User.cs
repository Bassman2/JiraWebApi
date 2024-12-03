namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA user. 
/// </summary>
public class User
{
    /// <summary>
    /// Automatic assignee for AssignIssueAsync.
    /// </summary>
    public static User AutomaticAssignee
    {
        get { return new User() { Name = "-1" }; }
    }

    /// <summary>
    /// Empty assignee for AssignIssueAsync.
    /// </summary>
    public static User EmptyAssignee
    {
        get { return new User() { Name = null }; }
    }

    /// <summary>
    /// Url of the REST item.
    /// </summary>
    public Uri? Self { get; internal set; }

    /// <summary>
    /// Name of the JIRA user.
    /// </summary>
    public string? Name { get; internal set; }

    /// <summary>
    /// E-mail address of the JIRA user.
    /// </summary>
    public string? EmailAddress { get; internal set; }

    /// <summary>
    /// Avatar URLs of the JIRA user.
    /// </summary>
    public AvatarUrls? AvatarUrls { get; internal set; }

    /// <summary>
    /// Display name of the JIRA user.
    /// </summary>
    public string? DisplayName { get; internal set; }

    /// <summary>
    /// Signals if the JIRA user is actve.
    /// </summary>
    public bool IsActive { get; internal set; }

    /// <summary>
    /// Time zone of the JIRA user.
    /// </summary>
    public string? TimeZone { get; internal set; }
}
