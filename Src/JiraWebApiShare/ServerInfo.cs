namespace JiraWebApi;

/// <summary>
/// Informations about the JIRA server.
/// </summary>
public sealed class ServerInfo
{
    /// <summary>
    /// Initializes a new instance of the ServerInfo class.
    /// </summary>
    internal ServerInfo(ServerInfoModel model)
    {
        BaseUrl = model.BaseUrl!;
        Version = new Version(model.Version ?? "0.0.0");
        DeploymentType = model.DeploymentType ?? "";
        BuildDate = model.BuildDate;
        ServerTime = model.ServerTime;
        ScmInfo = model.ScmInfo ?? "";
        ServerTitle = model.ServerTitle!;
    }

    /// <summary>
    /// Base web address of the JIRA server.
    /// </summary>
    public Uri BaseUrl { get; }

    /// <summary>
    /// Build version of the JIRA server release version.
    /// </summary>
    public Version Version { get; }

    /// <summary>
    /// Deployment type JIRA server.
    /// </summary>
    public string DeploymentType { get; }

    /// <summary>
    /// Build date of the JIRA server release version.
    /// </summary>
    public DateTime? BuildDate { get; }

    /// <summary>
    /// Current server time.
    /// </summary>
    public DateTime? ServerTime { get; }

    /// <summary>
    /// Time zone of the server location.
    /// </summary>
    public string ScmInfo { get; }
    
    /// <summary>
    /// Title of the JIRA server.
    /// </summary>
    public string ServerTitle { get; }
}
