namespace JiraWebApi.Service.Model;

/// <summary>
/// Rrepresentation of a JIRA project. 
/// </summary>
internal  class ProjectModel : ComparableElementModel
{
   

    /// <summary>
    /// Support of the JQL 'projectsLeadByUser()' operator in LINQ.
    /// </summary>
    /// <returns>Not used.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static Project[] ProjectsLeadByUser()
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Support of the JQL 'projectsWhereUserHasPermission()' operator in LINQ.
    /// </summary>
    /// <returns>Not used.</returns>
    /// <remarks>For Linq use only.</remarks>
    [JqlFunction("projectsWhereUserHasPermission")]
    public static Project[] ProjectsWhereUserHasPermission()
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Support of the JQL 'projectsWhereUserHasRole()' operator in LINQ.
    /// </summary>
    /// <returns>Not used.</returns>
    /// <remarks>For Linq use only.</remarks>
    [JqlFunction("projectsWhereUserHasRole")]
    public static Project[] ProjectsWhereUserHasRole()
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }
    
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
    public IEnumerable<IssueType>? IssueTypes { get; set; }

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

    public static implicit operator Project?(ProjectModel? model)
    {
        return model is null ? null : new Project()
        {
            Self = model.Self,
            Id = model.Id,
            Name = model.Name,
            Key = model.Key,
            Description = model.Description,
            IconUrl = model.IconUrl,
            Lead = model.Lead,
            Components = model.Components,
            IssueTypes = model.IssueTypes,
            Url = model.Url,
            Email = model.Email,
            AssigneeType = model.AssigneeType,
            Versions = model.Versions,
            Roles = model.Roles,
            AvatarUrls = model.AvatarUrls
        };
    }
}
