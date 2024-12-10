namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA project. 
/// </summary>
[DebuggerDisplay("{Id} {Key} {Name}")]
public sealed class Project
{
    private readonly JiraService? service;

    internal Project(JiraService service, ProjectModel model)
    {
        this.service = service;
        Self = model.Self;
        Id = model.Id!;
        Key = model.Key!;
        Name = model.Name;
        Description = model.Description;
        IconUrl = model.IconUrl;
        Lead = model.Lead.CastModel<User>();
        Components = model.Components.CastModel<Component>();
        IssueTypes = model.IssueTypes.CastModel<IssueType>();
        Url = model.Url;
        Email = model.Email;
        AssigneeType = model.AssigneeType;
        Versions = model.Versions;
        //Roles = model.Roles;
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
    public string Id { get; set; }
     
    /// <summary>
    /// Key of the JIRY project.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    public string? Name { get; set; }

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
    //public Roles? Roles { get; set; }

    /// <summary>
    /// Avatar URLs of the JIRY project.
    /// </summary>
    public AvatarUrls? AvatarUrls { get; set; }

    #endregion
    
    public async Task<CreateMeta?> GetCreateMetaAsync(string issueTypeId, CancellationToken cancellationToken = default)
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

    public async Task<Issue?> CreateIssueAsync(IssueType issueType, string reporter, string summary, string description, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);
        
        ArgumentNullException.ThrowIfNull(issueType, nameof(issueType));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(issueType.Id, nameof(issueType.Id));

        CreateIssueModel model = new() { Fields = [] };
        model.Fields.Add("project", new ProjectModel() { Id = this.Id });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueType.Id });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await service.CreateIssueAsync(model, cancellationToken);
        return res.CastModel<Issue>(service);
    }

    public async Task<IEnumerable<Component>?> GetComponentsAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetComponentsAsync(Id, cancellationToken);
        return res.CastModel<Component>();
    }
}
