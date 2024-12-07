namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA project. 
/// </summary>
[DebuggerDisplay("{Id} {Key} {Name}")]
public sealed class Project
{
    private readonly JiraService? service;

    public Project()
    {

    }

    public Project(object a, object b)
    {

    }

    internal Project(JiraService service, ProjectModel model)
    {
        this.service = service;
        Self = model.Self;
        Id = model.Id;
        Name = model.Name;
        Key = model.Key!;
        Description = model.Description;
        IconUrl = model.IconUrl;
        Lead = model.Lead;
        Components = model.Components;
        IssueTypes = model.IssueTypes;
        Url = model.Url;
        Email = model.Email;
        AssigneeType = model.AssigneeType;
        Versions = model.Versions;
        Roles = model.Roles;
        AvatarUrls = model.AvatarUrls;
    }

    #region Properties

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
    
    /// <summary>
    /// Key of the JIRY project.
    /// </summary>
    public string Key { get; }
            
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

    #endregion

    public async Task<CreateMeta?> GetCreateMetaAsync(string projectKey, string issueTypeId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetCreateMetaAsync(this.Key, issueTypeId, cancellationToken);
        return res;
    }

    public async Task<CreateMeta?> GetEditMetaAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetEditMetaAsync(issueIdOrKey, cancellationToken);
        return res;
    }

}
