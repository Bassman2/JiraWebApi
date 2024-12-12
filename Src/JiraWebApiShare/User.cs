namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA user. 
/// </summary>
public class User
{
    internal User(UserModel model)
    {
        Self = model.Self;
        Key = model.Key;
        Name = model.Name;
        EmailAddress = model.EmailAddress;
        AvatarUrls = model.AvatarUrls.CastModel<AvatarUrls>(); 
        DisplayName = model.DisplayName;
        IsActive = model.IsActive;
        IsDeleted = model.IsDeleted;
        TimeZone = model.TimeZone;
        Locale = model.Locale;
    }

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
    public Uri? Self { get; }

    /// <summary>
    /// Key of the REST item.
    /// </summary>
    public string? Key { get; }

    /// <summary>
    /// Name of the JIRA user.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// E-mail address of the JIRA user.
    /// </summary>
    public string? EmailAddress { get; }

    /// <summary>
    /// Avatar URLs of the JIRA user.
    /// </summary>
    public AvatarUrls? AvatarUrls { get; }

    /// <summary>
    /// Display name of the JIRA user.
    /// </summary>
    public string? DisplayName { get; }

    /// <summary>
    /// Signals if the JIRA user is actve.
    /// </summary>
    public bool IsActive { get; }

    /// <summary>
    /// Signals if the JIRA user is deleted.
    /// </summary>
    public bool IsDeleted { get; }

    /// <summary>
    /// Time zone of the JIRA user.
    /// </summary>
    public string? TimeZone { get; }

    /// <summary>
    /// Time zone of the JIRA user.
    /// </summary>
    public string? Locale { get; }
}
