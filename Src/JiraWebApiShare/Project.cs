namespace JiraWebApi;

/// <summary>
/// Rrepresentation of a JIRA project. 
/// </summary>
[DebuggerDisplay("{Id} {Key} {Name}")]
public sealed class Project
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    public string? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    public string? Name { get; set; }

    ///// <summary>
    ///// Initializes a new instance of the Project class.
    ///// </summary>
    //internal Project()
    //{ }

    ///// <summary>
    ///// Support of the JQL 'projectsLeadByUser()' operator in LINQ.
    ///// </summary>
    ///// <returns>Not used.</returns>
    ///// <remarks>For Linq use only.</remarks>
    //public static Project[] ProjectsLeadByUser()
    //{
    //    throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    //}

    ///// <summary>
    ///// Support of the JQL 'projectsWhereUserHasPermission()' operator in LINQ.
    ///// </summary>
    ///// <returns>Not used.</returns>
    ///// <remarks>For Linq use only.</remarks>
    //[JqlFunction("projectsWhereUserHasPermission")]
    //public static Project[] ProjectsWhereUserHasPermission()
    //{
    //    throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    //}

    ///// <summary>
    ///// Support of the JQL 'projectsWhereUserHasRole()' operator in LINQ.
    ///// </summary>
    ///// <returns>Not used.</returns>
    ///// <remarks>For Linq use only.</remarks>
    //[JqlFunction("projectsWhereUserHasRole")]
    //public static Project[] ProjectsWhereUserHasRole()
    //{
    //    throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    //}

    /// <summary>
    /// Key of the JIRY project.
    /// </summary>
    public string? Key { get; set; }
            
    /// <summary>
    /// Description of the JIRY project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Icon URL of the JIRY project.
    /// </summary>
    public Uri? IconUrl { get; set; }

    /// <summary>
    /// Lead of the JIRY project.
    /// </summary>
    public User? Lead { get; set; }

    /// <summary>
    /// Components of the JIRY project.
    /// </summary>
    public IEnumerable<Component>? Components { get; set; }

    /// <summary>
    /// Issue types of the JIRY project.
    /// </summary>
    public IEnumerable<IssueType>? IssueTypes { get; set; }

    /// <summary>
    /// URL of the JIRY project.
    /// </summary>
    public Uri? Url { get; set; }

    /// <summary>
    /// E-mail of the JIRY project.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Assignee type of the JIRY project.
    /// </summary>
    public string? AssigneeType { get; set; }

    /// <summary>
    /// Versions of the JIRY project.
    /// </summary>
    public IEnumerable<IssueVersion>? Versions { get; set; }

    /// <summary>
    /// Roles of the JIRY project.
    /// </summary>
    public Roles? Roles { get; set; }

    /// <summary>
    /// Avatar URLs of the JIRY project.
    /// </summary>
    public AvatarUrls? AvatarUrls { get; set; }
}
