namespace JiraWebApi;

/// <summary>
/// Rrepresentation of a JIRA project. 
/// </summary>
public sealed class Project : ComparableElementModel
{
    /// <summary>
    /// Initializes a new instance of the Project class.
    /// </summary>
    internal Project()
    { }

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
    public string Key { get; private set; }
            
    /// <summary>
    /// Description of the JIRY project.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; private set; }

    /// <summary>
    /// Icon URL of the JIRY project.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri IconUrl { get; private set; }

    /// <summary>
    /// Lead of the JIRY project.
    /// </summary>
    [JsonPropertyName("lead")]
    public User Lead { get; private set; }

    /// <summary>
    /// Components of the JIRY project.
    /// </summary>
    [JsonPropertyName("components")]
    public IEnumerable<Component> Components { get; private set; }

    /// <summary>
    /// Issue types of the JIRY project.
    /// </summary>
    [JsonPropertyName("issueTypes")]
    public IEnumerable<IssueType> IssueTypes { get; private set; }

    /// <summary>
    /// URL of the JIRY project.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri Url { get; private set; }

    /// <summary>
    /// E-mail of the JIRY project.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; private set; }

    /// <summary>
    /// Assignee type of the JIRY project.
    /// </summary>
    [JsonPropertyName("assigneeType")]
    public string AssigneeType { get; private set; }

    /// <summary>
    /// Versions of the JIRY project.
    /// </summary>
    [JsonPropertyName("versions")]
    public IEnumerable<IssueVersion> Versions { get; private set; }

    /// <summary>
    /// Roles of the JIRY project.
    /// </summary>
    [JsonPropertyName("roles")]
    public Roles roles { get; private set; }

    /// <summary>
    /// Avatar URLs of the JIRY project.
    /// </summary>
    [JsonPropertyName("avatarUrls")]
    public AvatarUrls AvatarUrls { get; private set; }
}
