namespace JiraWebApi;

/// <summary>
/// Informations about the JIRA server.
/// </summary>
public sealed class ServerInfo
{
    /// <summary>
    /// Initializes a new instance of the ServerInfo class.
    /// </summary>
    internal ServerInfo()
    { }

    /// <summary>
    /// Base web address of the JIRA server.
    /// </summary>
    public required Uri BaseUrl { get; init; }

    /// <summary>
    /// Build version of the JIRA server release version.
    /// </summary>
    public required Version Version { get; init; }

    /// <summary>
    /// Deployment type JIRA server.
    /// </summary>
    public required string DeploymentType { get; init; }


    /// <summary>
    /// Build date of the JIRA server release version.
    /// </summary>
    public required DateTime BuildDate { get; init; }

    /// <summary>
    /// Current server time.
    /// </summary>
    public required DateTime ServerTime { get; init; }

    /// <summary>
    /// Time zone of the server location.
    /// </summary>
    public required string ScmInfo { get; init; }
    
    /// <summary>
    /// Title of the JIRA server.
    /// </summary>
    public required string ServerTitle { get; init; }
}
