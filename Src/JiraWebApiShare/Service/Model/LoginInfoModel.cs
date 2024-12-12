namespace JiraWebApi.Service.Model;


/// <summary>
/// Representation of a JIRA login info. 
/// </summary>
internal class LoginInfoModel
{
    /// <summary>
    /// Number of failed logins of the current user.
    /// </summary>
    public int FailedLoginCount { get; init; }
    
    /// <summary>
    /// Number of logins of the current user.
    /// </summary>
    public int LoginCount { get; init; }

    /// <summary>
    /// Date of the last failed login of the current user.
    /// </summary>
    public string? LastFailedLoginTime { get; init; }

    /// <summary>
    /// Date of the previous login of the current user.
    /// </summary>
    public string? PreviousLoginTime { get; init; }
}
