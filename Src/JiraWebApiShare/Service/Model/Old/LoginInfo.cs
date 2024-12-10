namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA login info. 
/// </summary>
internal sealed class LoginInfo
{
    /// <summary>
    /// Number of failed logins of the current user.
    /// </summary>
    public int FailedLoginCount { get; internal init; }
    
    /// <summary>
    /// Number of logins of the current user.
    /// </summary>
    public int LoginCount { get; internal init; }

    /// <summary>
    /// Date of the last failed login of the current user.
    /// </summary>
    public string? LastFailedLoginTime { get; internal init; }

    /// <summary>
    /// Date of the previous login of the current user.
    /// </summary>
    public string? PreviousLoginTime { get; internal init; }
}
