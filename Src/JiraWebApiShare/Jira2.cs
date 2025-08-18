namespace JiraWebApi;

/// <summary>
/// Represents a JIRA client that provides access to various JIRA resources and operations.
/// </summary>
public sealed class Jira2 : JsonService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Artifactory2"/> class using a store key and application name.
    /// </summary>
    /// <param name="storeKey">The key used to store authentication or configuration data.</param>
    /// <param name="appName">The name of the application using the GitHub API.</param>
    public Jira2(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Artifactory2"/> class using a host URI, an optional authenticator, and an application name.
    /// </summary>
    /// <param name="host">The base URI of the GitHub API host.</param>
    /// <param name="authenticator">The authenticator used for API authentication, or <c>null</c> for unauthenticated access.</param>
    /// <param name="appName">The name of the application using the GitHub API.</param>
    public Jira2(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    /// <summary>
    /// Gets the URL used to test authentication with the Artifactory API.
    /// </summary>
    protected override string? AuthenticationTestUrl => "/rest/api/2/serverInfo";

    /// <summary>
    /// Handles errors returned from HTTP responses by reading the error content and throwing a <see cref="WebServiceException"/>.
    /// </summary>
    /// <param name="response">The HTTP response message containing the error.</param>
    /// <param name="memberName">The name of the member where the error occurred.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    {
        if (response.Content is JsonContent)
        {
            //var error = await ReadFromJsonAsync<ErrorModel>(response, cancellationToken);

            JsonTypeInfo<ErrorModel> jsonTypeInfoOut = (JsonTypeInfo<ErrorModel>)context.GetTypeInfo(typeof(ErrorModel))!;
            var error = await response.Content.ReadFromJsonAsync<ErrorModel>(jsonTypeInfoOut, cancellationToken);

            throw new WebServiceException(error?.ToString(), response.RequestMessage?.RequestUri, response.StatusCode, response.ReasonPhrase, memberName);
        }
        throw new WebServiceException("", response.RequestMessage?.RequestUri, response.StatusCode, response.ReasonPhrase, memberName);
    }

    #region Attachments

    ///// <summary>
    ///// Returns the meta informations for an attachments, specifically if they are enabled and the maximum upload size allowed.
    ///// </summary>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<AttachmentMetaModel?> GetAttachmentMetaAsync(CancellationToken cancellationToken)
    //{
    //    var res = await GetFromJsonAsync<AttachmentMetaModel>("rest/api/2/attachment/meta", cancellationToken);
    //    return res;
    //}

    ///// <summary>
    ///// Returns the meta-data for an attachment, including the URI of the actual attached file.
    ///// </summary>
    ///// <param name="attachmentId">The id of the attachment to get.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<AttachmentModel?> GetAttachmentAsync(string attachmentId, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(attachmentId, nameof(attachmentId));

    //    var res = await GetFromJsonAsync<AttachmentModel>($"rest/api/2/attachment/{attachmentId}", cancellationToken);
    //    return res;
    //}

    ///// <summary>
    ///// Remove an attachment from an issue.
    ///// </summary>
    ///// <param name="attachmentId">The id of the attachment to delete.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task DeleteAttachmentAsync(string attachmentId, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(attachmentId, nameof(attachmentId));

    //    await DeleteAsync($"rest/api/2/attachment/{attachmentId}", cancellationToken);
    //}

    ///// <summary>
    ///// Add one or more attachments to an issue.
    ///// </summary>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="files">List with attachments to add.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<IEnumerable<AttachmentModel>?> AddAttachmentsAsync(string issueIdOrKey, IEnumerable<KeyValuePair<string, Stream>> files, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNull(files, nameof(files));

    //    var res = await PostFilesFromJsonAsync<IEnumerable<AttachmentModel>>($"rest/api/2/issue/{issueIdOrKey}/attachments", files, cancellationToken);
    //    return res;
    //}

    ///// <summary>
    ///// Get the date stream of an attachment.
    ///// </summary>
    ///// <param name="attachmentUrl">Url of the attachment.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<Stream> GetAttachmentStreamAsync(Uri attachmentUrl, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNull(attachmentUrl, nameof(attachmentUrl));

    //    return await GetFromStreamAsync(attachmentUrl.ToString(), cancellationToken);
    //}

    #endregion

    #region Comment

    ///// <summary>
    ///// Returns all comments for an issue.
    ///// </summary>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<IEnumerable<CommentModel>?> GetCommentsAsync(string issueIdOrKey, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

    //    var res = await GetFromJsonAsync<CommentListModel>($"rest/api/2/issue/{issueIdOrKey}/comment", cancellationToken);
    //    return res?.Comments;
    //}

    ///// <summary>
    ///// Adds a new comment to an issue.
    ///// </summary>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="comment">Comment class to add.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<CommentModel?> AddCommentAsync(string issueIdOrKey, CommentModel comment, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNull(comment, nameof(comment));

    //    var res = await PostAsJsonAsync<CommentModel, CommentModel>($"rest/api/2/issue/{issueIdOrKey}/comment", comment, cancellationToken);
    //    return res;
    //}

    ///// <summary>
    ///// Returns all comments for an issue.
    ///// </summary>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="commentId">Id of the comment.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<CommentModel?> GetCommentAsync(string issueIdOrKey, string commentId, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNullOrEmpty(commentId, nameof(commentId));

    //    var res = await GetFromJsonAsync<CommentModel>($"rest/api/2/issue/{issueIdOrKey}/comment/{commentId}", cancellationToken);
    //    return res;
    //}

    ///// <summary>
    ///// Updates an existing comment.
    ///// </summary>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="comment">Class of the comment to update.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    ///// <remarks>Json bug in JIRA 5.0.4</remarks>
    //public async Task<CommentModel?> UpdateCommentAsync(string issueIdOrKey, CommentModel comment, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNull(comment, nameof(comment));

    //    // TODO find a way to suppress deserialize
    //    DateTime? created = comment.Created;
    //    DateTime? updated = comment.Updated;
    //    comment.Created = null;
    //    comment.Updated = null;
    //    CommentModel? res = await PutAsJsonAsync<CommentModel, CommentModel>($"rest/api/2/issue/{issueIdOrKey}/comment/{comment.Id}", comment, cancellationToken);
    //    comment.Created = created;
    //    comment.Updated = updated;
    //    return res;
    //}

    ///// <summary>
    ///// Deletes an existing comment.
    ///// </summary>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="commentId">Id of the comment.</param>
    ///// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task DeleteCommentAsync(string issueIdOrKey, string commentId, CancellationToken cancellationToken)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNullOrEmpty(commentId, nameof(commentId));

    //    await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/comment/{commentId}", cancellationToken);
    //}

    #endregion

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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<ComponentModel>>($"rest/api/2/project/{project.Id}/components", cancellationToken);

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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ComponentModel>($"rest/api/2/component/{componentId}", cancellationToken);
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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="issueIdOrKey"/> is <c>null</c> or empty.</exception>
    public async Task<Issue?> GetIssueAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(issueIdOrKey, nameof(issueIdOrKey));

        try
        {
            var res = await GetFromJsonAsync<IssueModel>($"rest/api/2/issue/{issueIdOrKey}", cancellationToken);
            return res.CastModel<Issue>(this); 
        }
        catch (WebServiceException ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            throw;
        }
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
        WebServiceException.ThrowIfNotConnected(client);
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

        IssueModel? res = await PostAsJsonAsync<IssueModel, IssueModel>("rest/api/2/issue", model, cancellationToken);

        return res.CastModel<Issue>(this);
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
        WebServiceException.ThrowIfNotConnected(client);

        IssueModel model = new() { Fields = [] };
        model.Fields.Add("parent", new IssueModel() { Key = parentKey });
        model.Fields.Add("project", new ProjectModel() { Id = projectId });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueTypeId });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await PostAsJsonAsync<IssueModel, IssueModel>("rest/api/2/issue", model, cancellationToken);
        return res.CastModel<Issue>(this);
    }

    #endregion

    #region IssueType

    /// <summary>
    /// Returns a list of all issue types visible to the user.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueType>?> GetIssueTypesAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<IssueTypeModel>>("rest/api/2/issuetype", cancellationToken);
        return res.CastModel<IssueType>();
    }

    /// <summary>
    /// Retrieves a specific issue type by its name from the JIRA server.
    /// </summary>
    /// <param name="issueTypeId">The issueTypeId of the issue type to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="IssueType"/> object,
    /// or <c>null</c> if the issue type is not found.
    /// </returns>
    public async Task<IssueType?> GetIssueTypeAsync(string issueTypeId, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IssueTypeModel>($"rest/api/2/issuetype/{issueTypeId}", cancellationToken);
        return res.CastModel<IssueType>();
    }

    #endregion

    #region Priority

    /// <summary>
    /// Returns a list of all issue priorities.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<Priority>?> GetPrioritiesAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<PriorityModel>>("rest/api/2/priority", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<PriorityModel>($"rest/api/2/priority/{priorityId}", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        long total = 0;
        long startAt = 0;
        int maxResults = 100;
        while (startAt > total)
        {
            string requestUri = CombineUrl("rest/api/2/priority/page", ("startAt", startAt), ("maxResults", maxResults));
            var res = await GetFromJsonAsync<PriorityPageModel>(requestUri, cancellationToken);
            if (res == null || res.Values == null)
            {
                yield break;
            }
            foreach (var item in res.Values)
            {
                yield return item.CastModel<Priority>()!;
            }
            total = res.Total;
            startAt = res.Values.Count();
            //if (res == null)
            //{
            //    yield break;
            //}
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<ProjectModel>>("rest/api/2/project", cancellationToken);
        return res.CastModel<Project>(this); 
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
    public async Task<Project?> GetProjectByKeyAsync(string projectIdOrKey, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ProjectModel>($"rest/api/2/project/{projectIdOrKey}", cancellationToken);
        return res.CastModel<Project>(this);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<ProjectTypeModel>>("rest/api/2/project/type", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ProjectTypeModel>($"rest/api/2/project/type/{projectTypeKey}", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ProjectTypeModel>($"rest/api/2/project/type/{projectTypeKey}/accessible", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<CreateMetaModel>($"rest/api/2/issue/createmeta/{projectKey}/issuetypes/{issueType}", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<CreateMetaModel>($"rest/api/2/issue/{issueIdOrKey}/editmeta", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<ResolutionModel>>("rest/api/2/resolution", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ResolutionModel>($"rest/api/2/resolution/{resolutionId}", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ServerInfoModel>("/rest/api/2/serverInfo", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<StatusModel>>("rest/api/2/status", cancellationToken);
        return res.CastModel<Status>();
    }

    /// <summary>
    /// Retrieves a specific issue status by its unique identifier.
    /// </summary>
    /// <param name="statusIdOrName">The ID of the status to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Status"/> object,
    /// or <c>null</c> if the status is not found.
    /// </returns>
    public async Task<Status?> GetStatusAsync(string statusIdOrName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentNullException.ThrowIfNullOrEmpty(statusIdOrName, nameof(statusIdOrName));

        var res = await GetFromJsonAsync<StatusModel>($"rest/api/2/status/{statusIdOrName}", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<IEnumerable<StatusCategoryModel>>("rest/api/2/statuscategory", cancellationToken);
        return res.CastModel<StatusCategory>();
    }

    /// <summary>
    /// Retrieves a specific status category by its unique identifier from the JIRA server.
    /// </summary>
    /// <param name="statusCategoryIdOrName">The ID of the status category to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="StatusCategory"/> object,
    /// or <c>null</c> if the status category is not found.
    /// </returns>
    public async Task<StatusCategory?> GetStatusCategoryAsync(string statusCategoryIdOrName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentNullException.ThrowIfNullOrEmpty(statusCategoryIdOrName, nameof(statusCategoryIdOrName));

        var res = await GetFromJsonAsync<StatusCategoryModel>($"rest/api/2/statuscategory/{statusCategoryIdOrName}", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentNullException.ThrowIfNullOrEmpty(username, nameof(username));

        var res = await GetFromJsonAsync<UserModel>($"rest/api/2/user?username={username}", cancellationToken);
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
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<UserModel>("rest/api/2/myself", cancellationToken);
        return res.CastModel<User>(); 
    }

    #endregion

    #region Version
    
    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetFromJsonAsync<ServerInfoModel>("/rest/api/2/serverInfo", cancellationToken);
        return res?.Version;
    }

    #endregion

    #region Download

    /// <summary>
    /// Downloads a resource from the specified request URI and saves it to the given file path.
    /// </summary>
    /// <param name="requestUri">The URI of the resource to download.</param>
    /// <param name="filePath">The file path where the downloaded resource will be saved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous download operation.</returns>
    public async Task DownloadAsync(string requestUri, string filePath, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(requestUri, nameof(requestUri));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));


        await base.DownloadAsync(requestUri, filePath, cancellationToken);
    }

    /// <summary>
    /// Downloads a resource from the specified request URI and returns it as a stream.
    /// </summary>
    /// <param name="requestUri">The URI of the resource to download.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous download operation. The task result contains a <see cref="System.IO.Stream"/>
    /// representing the downloaded resource.
    /// </returns>
    public async Task<System.IO.Stream> DownloadAsync(string requestUri, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(requestUri, nameof(requestUri));

        return await GetFromStreamAsync(requestUri, cancellationToken);
    }

    #endregion
}
