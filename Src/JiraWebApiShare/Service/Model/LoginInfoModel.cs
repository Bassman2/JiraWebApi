namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA login info. 
/// </summary>
internal class LoginInfoModel
{
    /// <summary>
    /// Number of failed logins of the current user.
    /// </summary>
    [JsonPropertyName("failedLoginCount")]
    public int FailedLoginCount { get; set; }
    
    /// <summary>
    /// Number of logins of the current user.
    /// </summary>
    [JsonPropertyName("loginCount")]
    public int LoginCount { get; set; }

    /// <summary>
    /// Date of the last failed login of the current user.
    /// </summary>
    [JsonPropertyName("lastFailedLoginTime")]
    public string? LastFailedLoginTime { get; set; }

    /// <summary>
    /// Date of the previous login of the current user.
    /// </summary>
    [JsonPropertyName("previousLoginTime")]
    public string? PreviousLoginTime { get; set; }

    public static implicit operator LoginInfo?(LoginInfoModel? model)
    {
        return model is null ? null : new LoginInfo()
        {
            FailedLoginCount = model.FailedLoginCount,
            LoginCount = model.LoginCount,
            LastFailedLoginTime = model.LastFailedLoginTime,
            PreviousLoginTime = model.PreviousLoginTime
        };
    }
}
