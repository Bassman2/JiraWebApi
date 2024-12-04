namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA user. 
/// </summary>
public class User
{
    ///// <summary>
    ///// Automatic assignee for AssignIssueAsync.
    ///// </summary>
    //public static User AutomaticAssignee
    //{
    //    get { return new User() { Name = "-1" }; }
    //}

    ///// <summary>
    ///// Empty assignee for AssignIssueAsync.
    ///// </summary>
    //public static User EmptyAssignee
    //{
    //    get { return new User() { Name = null }; }
    //}

    /// <summary>
    /// Url of the REST item.
    /// </summary>
    public Uri? Self { get; init; }

    /// <summary>
    /// Key of the REST item.
    /// </summary>
    public string? Key { get; init; }

    /// <summary>
    /// Name of the JIRA user.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// E-mail address of the JIRA user.
    /// </summary>
    public string? EmailAddress { get; init; }

    /// <summary>
    /// Avatar URLs of the JIRA user.
    /// </summary>
    public AvatarUrls? AvatarUrls { get; init; }

    /// <summary>
    /// Display name of the JIRA user.
    /// </summary>
    public string? DisplayName { get; init; }

    /// <summary>
    /// Signals if the JIRA user is actve.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Signals if the JIRA user is deleted.
    /// </summary>
    public bool IsDeleted { get; init; }

    /// <summary>
    /// Time zone of the JIRA user.
    /// </summary>
    public string? TimeZone { get; init; }

    /// <summary>
    /// Time zone of the JIRA user.
    /// </summary>
    public string? Locale { get; init; }
}
