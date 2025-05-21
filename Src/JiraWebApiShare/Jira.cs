namespace JiraWebApi;

/// <summary>
/// Represents a JIRA client that provides access to various JIRA resources and operations.
/// </summary>
public sealed class Jira : IDisposable
{
    private JiraService? service;

    /// <summary>
    /// Initializes a new instance of the <see cref="Jira"/> class using a store key and application name.
    /// </summary>
    /// <param name="storeKey">The key to retrieve the host and token from the key store.</param>
    /// <param name="appName">The name of the application using the JIRA client.</param>
    public Jira(string storeKey, string appName)
        : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Jira"/> class using a host URI, token, and application name.
    /// </summary>
    /// <param name="host">The URI of the JIRA host.</param>
    /// <param name="token">The authentication token for accessing the JIRA API.</param>
    /// <param name="appName">The name of the application using the JIRA client.</param>
    public Jira(Uri host, string token, string appName)
    {
        service = new(host, new BearerAuthenticator(token), appName);
    }

    /// <summary>
    /// Disposes the resources used by the <see cref="Jira"/> instance.
    /// </summary>
    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }


    #region Component

    /// <summary>
    /// Retrieves a list of components for the specified project.
    /// </summary>
    /// <param name="project">The project for which to retrieve components.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of 
    /// <see cref="Component"/> objects representing the components of the project, or <c>null</c> if no components are found.
    /// </returns>
    public async Task<IEnumerable<Component>?> GetComponentsAsync(Project project, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetComponentsAsync(project.Id, cancellationToken);
        return res.CastModel<Component>();
    }

    /// <summary>
    /// Retrieves a specific component by its ID.
    /// </summary>
    /// <param name="componentId">The ID of the component to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Component"/> object, or <c>null</c> if the component is not found.
    /// </returns>
    public async Task<Component?> GetComponentAsync(long componentId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetComponentAsync(componentId, cancellationToken);
        return res.CastModel<Component>();
    }

    #endregion

    #region Issues

    /// <summary>
    /// Retrieves a specific issue by its key.
    /// </summary>
    /// <param name="issueKey">The key of the issue to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Issue"/> object, or <c>null</c> if the issue is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="issueKey"/> is <c>null</c> or empty.</exception>
    public async Task<Issue?> GetIssueAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(issueKey, nameof(issueKey));

        var res = await service.GetIssueAsync(issueKey, null, null, cancellationToken);
        return res.CastModel<Issue>(service);
    }

    /// <summary>
    /// Creates a new issue or sub-task in the specified project.
    /// </summary>
    /// <param name="project">The project where the issue will be created.</param>
    /// <param name="issueType">The type of the issue to create (e.g., Bug, Task, Sub-task).</param>
    /// <param name="reporter">The username of the reporter for the issue.</param>
    /// <param name="summary">A brief one-line summary of the issue.</param>
    /// <param name="description">A detailed description of the issue.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created issue
    /// as an <see cref="Issue"/>, or <c>null</c> if the creation failed.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="project"/>, <paramref name="issueType"/>, or <paramref name="summary"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="WebServiceException">
    /// Thrown if the JIRA service is not connected or is <c>null</c>.
    /// </exception>
    public async Task<Issue?> CreateIssueAsync(Project project, IssueType issueType, string reporter, string summary, string description, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);
        ArgumentNullException.ThrowIfNull(project, nameof(project));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(project.Id, nameof(project.Id));
        ArgumentNullException.ThrowIfNull(issueType, nameof(issueType));
        //ArgumentNullException.ThrowIfNullOrWhiteSpace(issueType.Id, nameof(issueType.Id));

        IssueModel model = new() { Fields = [] };
        model.Fields.Add("project", new ProjectModel() { Id = project.Id });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueType.Id });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await service.CreateIssueAsync(model, cancellationToken);
        return res.CastModel<Issue>(service);
    }

    /// <summary>
    /// Creates a new sub-issue (sub-task) under the specified parent issue in the given JIRA project.
    /// </summary>
    /// <param name="parentKey">The key of the parent issue under which the sub-issue will be created.</param>
    /// <param name="projectId">The ID of the project where the sub-issue will be created.</param>
    /// <param name="issueTypeId">The ID of the issue type for the sub-issue.</param>
    /// <param name="reporter">The username of the reporter for the sub-issue.</param>
    /// <param name="summary">A brief one-line summary of the sub-issue.</param>
    /// <param name="description">A detailed description of the sub-issue.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created sub-issue
    /// as an <see cref="Issue"/>, or <c>null</c> if the creation failed.
    /// </returns>
    /// <exception cref="WebServiceException">
    /// Thrown if the JIRA service is not connected or is <c>null</c>.
    /// </exception>
    public async Task<Issue?> CreateSubIssueAsync(string parentKey, string projectId, int issueTypeId, string reporter, string summary, string description, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        IssueModel model = new() { Fields = [] };
        model.Fields.Add("parent", new IssueModel() { Key = parentKey });
        model.Fields.Add("project", new ProjectModel() { Id = projectId });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueTypeId });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await service.CreateIssueAsync(model, cancellationToken);
        return res.CastModel<Issue>(service);
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
        return res.CastModel<IssueType>();
    }

    /// <summary>
    /// Retrieves a specific issue type by its name from the JIRA server.
    /// </summary>
    /// <param name="name">The name of the issue type to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="IssueType"/> object,
    /// or <c>null</c> if the issue type is not found.
    /// </returns>
    public async Task<IssueType?> GetIssueTypeAsync(string name, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetIssueTypesAsync(cancellationToken);
        var issueType = res?.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
        return issueType.CastModel<IssueType>();
    }

    #endregion

    #region Priority

    /// <summary>
    /// Returns a list of all issue priorities.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<Priority>?> GetPrioritiesAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetPrioritiesAsync(cancellationToken);
        return res.CastModel<Priority>();
    }

    /// <summary>
    /// Retrieves a specific issue priority by its unique identifier.
    /// </summary>
    /// <param name="priorityId">The ID of the priority to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Priority"/> object,
    /// or <c>null</c> if the priority is not found.
    /// </returns>
    public async Task<Priority?> GetPriorityAsync(int priorityId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetPriorityAsync(priorityId, cancellationToken);
        return res.CastModel<Priority>();
    }

    /// <summary>
    /// Retrieves a paginated asynchronous stream of all issue priorities from the JIRA server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous enumerable of <see cref="Priority"/> objects representing the issue priorities.
    /// </returns>
    /// <remarks>
    /// This method fetches priorities in pages, iterating through all available pages until all priorities are retrieved.
    /// </remarks>
    public async IAsyncEnumerable<Priority> GetPrioritiesPagedAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = service.GetPrioritiesPagedAsync(cancellationToken);
        //if (res == null)
        //{
        //    yield break;
        //}
        await foreach (var item in res)
        {
            yield return item.CastModel<Priority>()!;
        }
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
        return res.CastModel<Project>(this.service);  //res?.Select(static i => (Project)i!); 
    }

    /// <summary>
    /// Retrieves a specific project by its unique key from the JIRA server.
    /// </summary>
    /// <param name="projectKey">The key of the project to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Project"/> object,
    /// or <c>null</c> if the project is not found.
    /// </returns>
    public async Task<Project?> GetProjectByKeyAsync(string projectKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetProjectAsync(projectKey, cancellationToken);
        return res.CastModel<Project>(this.service);
    }

    #endregion

    #region ProjectType

    /// <summary>
    /// Retrieves a list of all project types available in the JIRA server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of
    /// <see cref="ProjectType"/> objects representing the available project types, or <c>null</c> if none are found.
    /// </returns>
    public async Task<IEnumerable<ProjectType>?> GetProjectTypesAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetProjectTypesAsync(cancellationToken);
        return res.CastModel<ProjectType>();
    }

    /// <summary>
    /// Retrieves a specific project type by its unique key from the JIRA server.
    /// </summary>
    /// <param name="projectTypeKey">The key of the project type to retrieve (e.g., "software", "business").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="ProjectType"/> object,
    /// or <c>null</c> if the project type is not found.
    /// </returns>
    public async Task<ProjectType?> GetProjectTypeAsync(string projectTypeKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetProjectTypeAsync(projectTypeKey, cancellationToken);
        return res.CastModel<ProjectType>();
    }

    /// <summary>
    /// Retrieves a specific project type by its unique key from the JIRA server, ensuring the current user has access to it.
    /// </summary>
    /// <param name="projectTypeKey">The key of the project type to retrieve (e.g., "software", "business").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="ProjectType"/> object,
    /// or <c>null</c> if the project type is not accessible or not found.
    /// </returns>
    public async Task<ProjectType?> GetAccessibleProjectTypeAsync(string projectTypeKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetAccessibleProjectTypeAsync(projectTypeKey, cancellationToken);
        return res.CastModel<ProjectType>();
    }

    #endregion

    #region Meta

    /// <summary>
    /// Retrieves the metadata required to create an issue of the specified type in the given JIRA project.
    /// </summary>
    /// <param name="projectKey">The key of the project for which to retrieve creation metadata.</param>
    /// <param name="issueType">The issue type for which to retrieve creation metadata.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="CreateMeta"/> object
    /// with the metadata needed to create an issue of the specified type, or <c>null</c> if no metadata is available.
    /// </returns>
    /// <exception cref="WebServiceException">
    /// Thrown if the JIRA service is not connected or is <c>null</c>.
    /// </exception>
    public async Task<CreateMeta?> GetCreateMetaAsync(string projectKey, IssueType issueType, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetCreateMetaAsync(projectKey, issueType.Id ?? 0, cancellationToken);
        return res.CastModel<CreateMeta>();
    }

    /// <summary>
    /// Retrieves the metadata required to edit an existing issue in the JIRA server.
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

    #endregion

    #region Resolution

    /// <summary>
    /// Retrieves a list of all issue resolutions from the JIRA server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of
    /// <see cref="Resolution"/> objects representing the available issue resolutions, or <c>null</c> if none are found.
    /// </returns>
    public async Task<IEnumerable<Resolution>?> GetResolutionsAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetResolutionsAsync(cancellationToken);
        return res.CastModel<Resolution>();
    }

    /// <summary>
    /// Retrieves a specific issue resolution by its unique identifier from the JIRA server.
    /// </summary>
    /// <param name="resolutionId">The ID of the resolution to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Resolution"/> object,
    /// or <c>null</c> if the resolution is not found.
    /// </returns>
    public async Task<Resolution?> GetResolutionAsync(int resolutionId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetResolutionAsync(resolutionId, cancellationToken);
        return res.CastModel<Resolution>();
    }

    #endregion

    #region ServerInfo

    /// <summary>
    /// Returns general information about the current JIRA server.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ServerInfo?> GetServerInfoAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetServerInfoAsync(cancellationToken);
        return res.CastModel<ServerInfo>();
    }

    #endregion

    #region Status

    /// <summary>
    /// Retrieves a list of all issue statuses from the JIRA server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of
    /// <see cref="Status"/> objects representing the available issue statuses, or <c>null</c> if no statuses are found.
    /// </returns>
    public async Task<IEnumerable<Status>?> GetStatusesAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetStatusesAsync(cancellationToken);
        return res.CastModel<Status>();
    }

    /// <summary>
    /// Retrieves a specific issue status by its unique identifier.
    /// </summary>
    /// <param name="statusId">The ID of the status to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Status"/> object,
    /// or <c>null</c> if the status is not found.
    /// </returns>
    public async Task<Status?> GetStatusAsync(int statusId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetStatusAsync(statusId.ToString(), cancellationToken);
        return res.CastModel<Status>();
    }

    /// <summary>
    /// Retrieves a specific issue status by its name from the JIRA server.
    /// </summary>
    /// <param name="statusName">The name of the status to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Status"/> object,
    /// or <c>null</c> if the status is not found.
    /// </returns>
    public async Task<Status?> GetStatusAsync(string statusName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetStatusAsync(statusName, cancellationToken);
        return res.CastModel<Status>();
    }

    #endregion

    #region StatusCategory

    /// <summary>
    /// Retrieves a list of all status categories from the JIRA server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of
    /// <see cref="StatusCategory"/> objects representing the available status categories, or <c>null</c> if none are found.
    /// </returns>
    public async Task<IEnumerable<StatusCategory>?> GetStatusCategoriesAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetStatusCategoriesAsync(cancellationToken);
        return res.CastModel<StatusCategory>();
    }

    /// <summary>
    /// Retrieves a specific status category by its unique identifier from the JIRA server.
    /// </summary>
    /// <param name="statusCategoryId">The ID of the status category to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="StatusCategory"/> object,
    /// or <c>null</c> if the status category is not found.
    /// </returns>
    public async Task<StatusCategory?> GetStatusCategoryAsync(int statusCategoryId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetStatusCategoryAsync(statusCategoryId.ToString(), cancellationToken);
        return res.CastModel<StatusCategory>();
    }

    /// <summary>
    /// Retrieves a specific status category by its name from the JIRA server.
    /// </summary>
    /// <param name="statusCategoryName">The name of the status category to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="StatusCategory"/> object,
    /// or <c>null</c> if the status category is not found.
    /// </returns>
    public async Task<StatusCategory?> GetStatusCategoryAsync(string statusCategoryName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetStatusCategoryAsync(statusCategoryName, cancellationToken);
        return res.CastModel<StatusCategory>();
    }

    #endregion

    #region User

    /// <summary>
    /// Retrieves a user by their username from the JIRA server.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="User"/> object,
    /// or <c>null</c> if the user is not found.
    /// </returns>
    /// <remarks>
    /// This resource cannot be accessed anonymously.
    /// </remarks>
    public async Task<User?> GetUserAsync(string username, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetUserAsync(username, cancellationToken);
        return res.CastModel<User>(); 
    }

    /// <summary>
    /// Retrieves information about the currently authenticated user from the JIRA server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="User"/> object
    /// representing the current user, or <c>null</c> if the user is not authenticated.
    /// </returns>
    /// <remarks>
    /// This method uses the JIRA REST API to fetch details about the currently logged-in user.
    /// </remarks>
    public async Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        var res = await service.GetCurrentUserAsync(cancellationToken);
        return res.CastModel<User>(); 
    }

    #endregion
}
