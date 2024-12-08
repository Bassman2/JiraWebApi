namespace JiraWebApi.Service.Model;

/// <summary>
/// Rrepresentation of a JIRA project. 
/// </summary>
internal class ProjectModel 
{
    public ProjectModel()
    { }

    /// <summary>
    /// Url of the JIRA REST project.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    /// <summary>
    /// Id of the JIRA project.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Name of the JIRA project.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Key of the JIRY project.
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }
            
    /// <summary>
    /// Description of the JIRY project.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Icon URL of the JIRY project.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri? IconUrl { get; set; }

    /// <summary>
    /// Lead of the JIRY project.
    /// </summary>
    [JsonPropertyName("lead")]
    public UserModel? Lead { get; set; }

    /// <summary>
    /// Components of the JIRY project.
    /// </summary>
    [JsonPropertyName("components")]
    public IEnumerable<Component>? Components { get; set; }

    /// <summary>
    /// Issue types of the JIRY project.
    /// </summary>
    [JsonPropertyName("issueTypes")]
    public IEnumerable<IssueTypeModel>? IssueTypes { get; set; }

    /// <summary>
    /// URL of the JIRY project.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    /// <summary>
    /// E-mail of the JIRY project.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Assignee type of the JIRY project.
    /// </summary>
    [JsonPropertyName("assigneeType")]
    public string? AssigneeType { get; set; }

    /// <summary>
    /// Versions of the JIRY project.
    /// </summary>
    [JsonPropertyName("versions")]
    public IEnumerable<IssueVersion>? Versions { get; set; }

    /// <summary>
    /// Roles of the JIRY project.
    /// </summary>
    [JsonPropertyName("roles")]
    public Roles? Roles { get; set; }

    /// <summary>
    /// Avatar URLs of the JIRY project.
    /// </summary>
    [JsonPropertyName("avatarUrls")]
    public AvatarUrlsModel? AvatarUrls { get; set; }
}
