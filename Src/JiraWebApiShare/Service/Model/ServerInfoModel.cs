namespace JiraWebApi.Service.Model;

/// <summary>
/// Informations about the JIRA server.
/// </summary>
internal class ServerInfoModel
{
    /// <summary>
    /// Base web address of the JIRA server.
    /// </summary>
    [JsonPropertyName("baseUrl")]
    public Uri? BaseUrl { get;  set; }

    /// <summary>
    /// Build version of the JIRA server release version.
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get;  set; }

    /// <summary>
    /// Build number of the JIRA server release version.
    /// </summary>
    [JsonPropertyName("deploymentType")]
    public string? DeploymentType { get; set; }
       
    /// <summary>
    /// Build date of the JIRA server release version.
    /// </summary>
    [JsonPropertyName("buildDate")]
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime? BuildDate { get; set; }

    /// <summary>
    /// Build date of the JIRA server release version.
    /// </summary>
    [JsonPropertyName("databaseBuildNumber")]
    public int DatabaseBuildNumber { get; set; }

    /// <summary>
    /// Current server time.
    /// </summary>
    [JsonPropertyName("serverTime")]
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime? ServerTime { get; set; }

    /// <summary>
    /// Time zone of the server location.
    /// </summary>
    [JsonPropertyName("scmInfo")]
    public string? ScmInfo { get; set; }

    /// <summary>
    /// Title of the JIRA server.
    /// </summary>
    [JsonPropertyName("serverTitle")]
    public string? ServerTitle { get; set; }

    public static implicit operator ServerInfo?(ServerInfoModel? model)
    {
        return model is null ? null : new ServerInfo()
        {
            BaseUrl = model.BaseUrl!,
            Version = new Version(model.Version ?? "0.0.0"),
            DeploymentType = model.DeploymentType ?? "",
            BuildDate = model.BuildDate ?? default,
            ServerTime = model.ServerTime ?? default,
            ScmInfo = model.ScmInfo ?? "",
            ServerTitle = model.ServerTitle!,
        };
    }
}
