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
        AvatarUrls = model.AvatarUrls.CastModel<AvatarUrls>();
    }

    #region Properties

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    public Uri? Self { get; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    public string Id { get; }
     
    /// <summary>
    /// Key of the JIRY project.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Description of the JIRY project.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Icon URL of the JIRY project.
    /// </summary>
    public Uri? IconUrl { get; }

    /// <summary>
    /// Lead of the JIRY project.
    /// </summary>
    public User? Lead { get; }

    /// <summary>
    /// Components of the JIRY project.
    /// </summary>
    public IEnumerable<Component>? Components { get; }

    /// <summary>
    /// Issue types of the JIRY project.
    /// </summary>
    public IEnumerable<IssueType>? IssueTypes { get; }

    /// <summary>
    /// URL of the JIRY project.
    /// </summary>
    public Uri? Url { get; }

    /// <summary>
    /// E-mail of the JIRY project.
    /// </summary>
    public string? Email { get; }

    /// <summary>
    /// Assignee type of the JIRY project.
    /// </summary>
    public string? AssigneeType { get; }

    /// <summary>
    /// Versions of the JIRY project.
    /// </summary>
    public IEnumerable<IssueVersion>? Versions { get; }

    // /// <summary>
    // /// Roles of the JIRY project.
    // /// </summary>
    //public Roles? Roles { get; set; }

    /// <summary>
    /// Avatar URLs of the JIRY project.
    /// </summary>
    public AvatarUrls? AvatarUrls { get; }

    #endregion

    /// <summary>
    /// Retrieves the metadata required to create an issue of the specified type in this JIRA project.
    /// </summary>
    /// <param name="issueTypeId">The ID of the issue type for which to retrieve creation metadata.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="CreateMeta"/> object
    /// with the metadata needed to create an issue of the specified type, or <c>null</c> if no metadata is available.
    /// </returns>
    /// <exception cref="WebServiceException">
    /// Thrown if the JIRA service is not connected or is <c>null</c>.
    /// </exception>
    public async Task<CreateMeta?> GetCreateMetaAsync(int issueTypeId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetCreateMetaAsync(this.Key, issueTypeId, cancellationToken);
        return res.CastModel<CreateMeta>();
    }

    /// <summary>
    /// Retrieves the metadata required to edit an existing issue in this JIRA project.
    /// </summary>
    /// <param name="issueIdOrKey">The ID or key of the issue for which to retrieve edit metadata.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="CreateMeta"/> object
    /// with the metadata needed to edit the specified issue, or <c>null</c> if no metadata is available.
    /// </returns>
    /// <exception cref="WebServiceException">
    /// Thrown if the JIRA service is not connected or is <c>null</c>.
    /// </exception>
    public async Task<CreateMeta?> GetEditMetaAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetEditMetaAsync(issueIdOrKey, cancellationToken);
        return res.CastModel<CreateMeta>();
    }

    /// <summary>
    /// Creates a new issue in this JIRA project with the specified type, reporter, summary, and description.
    /// </summary>
    /// <param name="issueType">The type of the issue to create.</param>
    /// <param name="reporter">The username of the reporter for the new issue.</param>
    /// <param name="summary">A brief one-line summary of the issue.</param>
    /// <param name="description">A detailed description of the issue.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created <see cref="Issue"/>,
    /// or <c>null</c> if the creation failed.
    /// </returns>
    /// <exception cref="WebServiceException">
    /// Thrown if the JIRA service is not connected or is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="issueType"/> is <c>null</c>.
    /// </exception>
    public async Task<Issue?> CreateIssueAsync(IssueType issueType, string reporter, string summary, string description, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);
        
        ArgumentNullException.ThrowIfNull(issueType, nameof(issueType));
        //ArgumentNullException.ThrowIfNullOrWhiteSpace(issueType.Id, nameof(issueType.Id));

        IssueModel model = new() { Fields = [] };
        model.Fields.Add("project", new ProjectModel() { Id = this.Id });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueType.Id });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await service.CreateIssueAsync(model, cancellationToken);
        return res.CastModel<Issue>(service);
    }

    /// <summary>
    /// Retrieves all components defined for this JIRA project.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of
    /// <see cref="Component"/> objects representing the components of the project, or <c>null</c> if no components are found.
    /// </returns>
    /// <exception cref="WebServiceException">
    /// Thrown if the JIRA service is not connected or is <c>null</c>.
    /// </exception>
    public async Task<IEnumerable<Component>?> GetComponentsAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetComponentsAsync(Id, cancellationToken);
        return res.CastModel<Component>();
    }
}
