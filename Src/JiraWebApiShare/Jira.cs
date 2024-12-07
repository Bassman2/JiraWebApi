namespace JiraWebApi;

public sealed class Jira : IDisposable
{
    private JiraService? service;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apikey"></param>    
    /// <exception cref="System.Security.Authentication.AuthenticationException">Thrown if authentication failed.</exception> 
    public Jira(Uri host, string apikey)
    {
        service = new JiraService(host, apikey);
    }

    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

    #region ServerInfo

    /// <summary>
    /// Returns general information about the current JIRA server.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ServerInfo?> GetServerInfoAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetServerInfoAsync(cancellationToken);
        return res;
    }

    #endregion

    #region Issues

    /// <summary>
    /// Creates an issue or a sub-task.
    /// </summary>
    /// <param name="issue">Issue class to create.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<Issue?> CreateIssueAsync(string projectId, string issueTypeId, string reporter, string summary, string description, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        CreateIssueModel model = new() { Fields = [] };
        model.Fields.Add("project", new ProjectModel() { Id = projectId });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueTypeId });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await service.CreateIssueAsync(model, cancellationToken);
        return res;
    }

    public async Task<Issue?> CreateSubIssueAsync(string parentKey, string projectId, string issueTypeId, string reporter, string summary, string description, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        CreateIssueModel model = new() { Fields = [] };
        model.Fields.Add("parent", new IssueModel() { Key = parentKey });
        model.Fields.Add("project", new ProjectModel() { Id = projectId });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueTypeId });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await service.CreateIssueAsync(model, cancellationToken);
        return res;
    }

    #endregion

    #region IssueType

    /// <summary>
    /// Returns a list of all issue types visible to the user.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueType>?> GetIssueTypesAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetIssueTypesAsync(cancellationToken);
        return res?.Select(static i => (IssueType)i!);
    }

    #endregion

    #region Project

    /// <summary>
    /// Returns all projects which are visible for the currently logged in user. If no user is logged in, it returns the list of projects that are visible when using anonymous access.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only the fields Self, Id, Key, Name and AvatarUrls will be filled by GetProjectsAsync. Call GetProjectAsync to get all fields. </remarks>
    public async Task<IEnumerable<Project>?> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetProjectsAsync(cancellationToken);
        return res.Cast<Project>(this.service);  //res?.Select(static i => (Project)i!); 
    }

    public async Task<Project?> GetProjectByKeyAsync(string projectKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetProjectAsync(projectKey, cancellationToken);
        return res.Cast<Project>(this.service);
    }

    #endregion

    #region Meta

    public async Task<CreateMeta?> GetCreateMetaAsync(string projectKey, string issueTypeId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetCreateMetaAsync(projectKey, issueTypeId, cancellationToken);
        return res;
    }

    public async Task<CreateMeta?> GetEditMetaAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetEditMetaAsync(issueIdOrKey, cancellationToken);
        return res;
    }

    #endregion

    #region User

    public async Task<User?> GetUserAsync(string username, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetUserAsync(username, cancellationToken);
        return res;
    }

    public async Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetCurrentUserAsync(cancellationToken);
        return res;
    }

    #endregion
}
