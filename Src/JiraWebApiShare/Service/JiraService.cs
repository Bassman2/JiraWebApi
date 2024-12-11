namespace JiraWebApi.Service;

// https://developer.atlassian.com/server/jira/platform/rest/v10002/api-group-serverinfo/#api-group-serverinfo
// https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-projects/#api-group-projects

// https://docs.atlassian.com/software/jira/docs/api/REST/9.12.0/

/// <summary>
/// JIRA Wep Api main class.
/// </summary>
internal class JiraService : JsonService
{
    //private readonly JiraQueryProvider provider;
    //this.provider = new JiraQueryProvider(this);

    /// <summary>
    /// Initializes a new instance of the Jira class.
    /// </summary>
    /// <param name="host">Host URL of the JIRA server.</param>
    /// <param name="apikey">API key.</param>
    /// <example>
    /// <code>
    /// using var jira = new Jira(new Uri("https://jira.atlassian.com"), "pokjfnlkfdskgkljgipooksdlksgölgklösg");
    /// </code>
    /// </example>
    /// <exception cref="System.Security.Authentication.AuthenticationException">Thrown if authentication failed.</exception> 
    public JiraService(Uri host, string apikey)
    : base(host, SourceGenerationContext.Default, new BearerAuthenticator(apikey))
    {
        //provider = new(this);
    }

    #region Private

    protected override void TestAutentication()
    {
        try
        {
            var _ = GetStringAsync("/rest/api/2/serverInfo", default).Result;
        }
        catch (Exception ex)
        {
            throw new AuthenticationException(ex.Message, ex);
        }
    }

    protected override async Task ErrorHandlingAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    {
        var error = await ReadFromJsonAsync<ErrorModel>(response, cancellationToken);
        throw new WebServiceException(error?.ToString(), response.RequestMessage?.RequestUri, response.StatusCode, response.ReasonPhrase, memberName);
    }

    //    private async IAsyncEnumerable<T> GetJsonAsyncEnumerableAsync<T, E>(string? requestUri, [EnumeratorCancellation] CancellationToken cancellationToken, [CallerMemberName] string memberName = "") where T : class where E : AsyncEnumerableModel 
    //    {
    //        ArgumentRequestUriException.ThrowIfNullOrWhiteSpace(requestUri, nameof(requestUri));



    //        while (requestUri != null)
    //        {
    //            var res = await GetFromJsonAsync<E>(requestUri, cancellationToken);

    //            foreach (var item in res)



    //            using HttpResponseMessage response = await client!.GetAsync(requestUri, cancellationToken);

    //#if DEBUG
    //            string str = await response.Content.ReadAsStringAsync(cancellationToken);
    //#endif
    //            if (!response.IsSuccessStatusCode)
    //            {
    //                await ErrorHandlingAsync(response, memberName, cancellationToken);
    //            }

    //            var res = await ReadFromJsonAsync<IEnumerable<T>?>(response, cancellationToken);
    //            if (res != null)
    //            {
    //                foreach (var item in res)
    //                {
    //                    yield return item;
    //                }
    //            }
    //            requestUri = NextLink(response);
    //        }
    //    }

    #endregion

    #region Linq

    //private IEnumerable<FieldModel?>? fields;
    //internal async Task<IEnumerable<FieldModel?>?> GetCachedFieldsAsync()
    //{
    //    return this.FieldModel ??= await GetFieldsAsync();
    //}


    //internal bool jqlTest = false;
    //internal string jqlQuery = "";
    //internal int? jqlStartAt = null;
    //internal int? jqlMaxResults = null;
    //internal IEnumerable<Issue>? GetIssuesFromJql(string? jql, int? startAt, int? maxResults)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(jql, nameof(jql));

    //    //{
    //    //    this.jqlQuery = jql;
    //    //    this.jqlStartAt = startAt;
    //    //    this.jqlMaxResults = maxResults;
    //    //    return new List<Issue>();
    //    //}

    //    return GetIssuesFromJqlAsync(jql, startAt.GetValueOrDefault(0), maxResults.GetValueOrDefault(500)).Result;
    //}

    /// <summary>
    /// Performs a issue search using LINQ
    /// </summary>
    /// <remarks>
    /// Methods: OrderBy, OrderByDescending, ThenBy, ThenByDescending, Skip, Take
    /// 
    /// </remarks>
    /// <example>
    /// var issues = (from issue in jira.Issues where issue.Priority == "Major" || issue.Priority == "Minor" select issue OrderBy issue.Version).Skip(5).Take(19);
    /// </example>
    //public IOrderedQueryable<Issue> Issues
    //{
    //    get { return new JiraQueryable<Issue>(this.provider); }
    //}

    #endregion

    #region Attachments

    /// <summary>
    /// Returns the meta informations for an attachments, specifically if they are enabled and the maximum upload size allowed.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<AttachmentMetaModel?> GetAttachmentMetaAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<AttachmentMetaModel>("rest/api/2/attachment/meta", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns the meta-data for an attachment, including the URI of the actual attached file.
    /// </summary>
    /// <param name="attachmentId">The id of the attachment to get.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<AttachmentModel?> GetAttachmentAsync(string attachmentId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(attachmentId, nameof(attachmentId));

        var res = await GetFromJsonAsync<AttachmentModel>($"rest/api/2/attachment/{attachmentId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Remove an attachment from an issue.
    /// </summary>
    /// <param name="attachmentId">The id of the attachment to delete.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteAttachmentAsync(string attachmentId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(attachmentId, nameof(attachmentId));

        await DeleteAsync($"rest/api/2/attachment/{attachmentId}", cancellationToken);
    }

    /// <summary>
    /// Add one or more attachments to an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="files">List with attachments to add.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<AttachmentModel>?> AddAttachmentsAsync(string issueIdOrKey, IEnumerable<KeyValuePair<string, Stream>> files, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(files, nameof(files));

        var res = await PostFilesFromJsonAsync<IEnumerable<AttachmentModel>>($"rest/api/2/issue/{issueIdOrKey}/attachments", files, cancellationToken);
        return res;
    }

    /// <summary>
    /// Get the date stream of an attachment.
    /// </summary>
    /// <param name="attachmentUrl">Url of the attachment.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<Stream> GetAttachmentStreamAsync(Uri attachmentUrl, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(attachmentUrl, nameof(attachmentUrl));

        return await GetFromStreamAsync(attachmentUrl.ToString(), cancellationToken);
    }

    #endregion

    #region Comment

    /// <summary>
    /// Returns all comments for an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<CommentModel>?> GetCommentsAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<CommentListModel>($"rest/api/2/issue/{issueIdOrKey}/comment", cancellationToken);
        return res?.Comments;
    }

    /// <summary>
    /// Adds a new comment to an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="comment">Comment class to add.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CommentModel?> AddCommentAsync(string issueIdOrKey, CommentModel comment, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(comment, nameof(comment));

        var res = await PostAsJsonAsync<CommentModel, CommentModel>($"rest/api/2/issue/{issueIdOrKey}/comment", comment, cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns all comments for an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="commentId">Id of the comment.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CommentModel?> GetCommentAsync(string issueIdOrKey, string commentId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(commentId, nameof(commentId));

        var res = await GetFromJsonAsync<CommentModel>($"rest/api/2/issue/{issueIdOrKey}/comment/{commentId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="comment">Class of the comment to update.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Json bug in JIRA 5.0.4</remarks>
    public async Task<CommentModel?> UpdateCommentAsync(string issueIdOrKey, CommentModel comment, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(comment, nameof(comment));

        // TODO find a way to suppress deserialize
        DateTime? created = comment.Created;
        DateTime? updated = comment.Updated;
        comment.Created = null;
        comment.Updated = null;
        CommentModel? res = await PutAsJsonAsync<CommentModel, CommentModel>($"rest/api/2/issue/{issueIdOrKey}/comment/{comment.Id}", comment, cancellationToken);
        comment.Created = created;
        comment.Updated = updated;
        return res;
    }

    /// <summary>
    /// Deletes an existing comment.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="commentId">Id of the comment.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteCommentAsync(string issueIdOrKey, string commentId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(commentId, nameof(commentId));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/comment/{commentId}", cancellationToken);
    }

    #endregion

    #region Component

    /// <summary>
    /// Contains a full representation of a the specified project's components.
    /// </summary>
    /// <param name="projectKey">Key of the project.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<ComponentModel>?> GetComponentsAsync(string projectIdOrKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(projectIdOrKey, nameof(projectIdOrKey));

        var res = await GetFromJsonAsync<IEnumerable<ComponentModel>>($"rest/api/2/project/{projectIdOrKey}/components", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a project component.
    /// </summary>
    /// <param name="componentId">Id of the component to get.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ComponentModel?> GetComponentAsync(long componentId, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<ComponentModel>($"rest/api/2/component/{componentId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Create a component.
    /// </summary>
    /// <param name="component">Component class to create.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ComponentModel?> CreateComponentAsync(ComponentModel component, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(component, nameof(component));

        var res = await PostAsJsonAsync<ComponentModel, ComponentModel>("rest/api/2/component", component, cancellationToken);
        return res;
    }



    /// <summary>
    /// Modify a component.
    /// </summary>
    /// <param name="component">Component class to update.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ComponentModel?> UpdateComponentAsync(ComponentModel component, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(component, nameof(component));

        var res = await PutAsJsonAsync<ComponentModel, ComponentModel>($"rest/api/2/component/{component.Id}", component, cancellationToken);
        return res;
    }

    /// <summary>
    /// Delete a project component.
    /// </summary>
    /// <param name="componentId">Id of the component to delete.</param>
    /// <param name="moveIssuesTo">The new component applied to issues whose 'id' component will be deleted. If this value is null, then the 'id' component is simply removed from the related isues.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteComponentAsync(int componentId, int moveIssuesTo, CancellationToken cancellationToken)
    {
        string req = CombineUrl($"rest/api/2/component/{componentId}", ("moveIssuesTo", moveIssuesTo == 0 ? null : moveIssuesTo));
        await DeleteAsync(req, cancellationToken);
    }

    /// <summary>
    /// Returns counts of issues related to this component.
    /// </summary>
    /// <param name="componentId">Id of the component.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueCountModel?> ComponentRelatedIssuesCountAsync(int componentId, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IssueCountModel>($"rest/api/2/component/{componentId}/relatedIssueCounts", cancellationToken);
        return res;
    }

    #endregion

    #region Field

    /// <summary>
    /// Returns a list of all fields, both System and Custom.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<FieldModel>?> GetFieldsAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<IEnumerable<FieldModel>>("rest/api/2/field", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a full representation of the Custom Field Option that has the given id.
    /// </summary>
    /// <param name="customFieldOptionId">Id of the custom field option.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CustomFieldOptionModel?> GetCustomFieldOptionAsync(string customFieldOptionId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(customFieldOptionId, nameof(customFieldOptionId));

        var res = await GetFromJsonAsync<CustomFieldOptionModel>($"rest/api/2/customFieldOption/{customFieldOptionId}", cancellationToken);
        return res;
    }

    #endregion

    #region Filter

    /// <summary>
    /// Creates a new filter, and returns newly created filter Currently sets permissions just using the users default sharing permissions.
    /// </summary>
    /// <param name="filter">Filter class to create.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task<FilterModel?> CreateFilterAsync(FilterModel filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));

        var res = await PostAsJsonAsync<FilterModel, FilterModel>("rest/api/2/filter", filter, cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns the favourite filters of the logged-in user.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<FilterModel>?> GetFilterFavouritesAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<IEnumerable<FilterModel>>("rest/api/2/filter/favourite", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a filter given an id.
    /// </summary>
    /// <param name="filterId">Id of the filter.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<FilterModel?> GetFilterAsync(string filterId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(filterId, nameof(filterId));

        var res = await GetFromJsonAsync<FilterModel>($"rest/api/2/filter/{filterId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Updates an existing filter, and returns its new value.
    /// </summary>
    /// <param name="filter">Filter class to update.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task<FilterModel?> UpdateFilterAsync(FilterModel filter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));

        var res = await PutAsJsonAsync<FilterModel, FilterModel>($"rest/api/2/filter/{filter.Id}", filter, cancellationToken);
        return res;
    }

    /// <summary>
    /// Delete a filter.
    /// </summary>
    /// <param name="filterId">Id of the filter to delete.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task DeleteFilterAsync(string filterId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(filterId, nameof(filterId));

        await DeleteAsync($"rest/api/2/filter/{filterId}", cancellationToken);
    }

    #endregion

    #region Group

    /// <summary>
    /// Get group with specified group name.
    /// </summary>
    /// <param name="groupName">Name of the group</param>
    /// <param name="expandGroup">Expand group parameter.</param>
    /// <returns>Jira group.</returns>
    /// <remarks>Only supported with JIRA 6.0 or later</remarks>
    public async Task<GroupModel?> GetGroupAsync(string groupName, string? expandGroup = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(groupName, nameof(groupName));

        string expand = string.IsNullOrEmpty(expandGroup) ? "" : $"&expand={expandGroup}";
        var res = await GetFromJsonAsync<GroupModel>($"rest/api/2/group?groupname={groupName}{expand}", cancellationToken);
        return res;
    }

    #endregion

    #region Issue

    /// <summary>
    /// Creates an issue or a sub-task.
    /// </summary>
    /// <param name="issue">Issue class to create.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    //public async Task<IssueModel?> CreateIssueAsync(IssueModel issue, CancellationToken cancellationToken = default)
    //{
    //    ArgumentNullException.ThrowIfNull(issue, nameof(issue));

    //    IssueModel? res = await PostAsJsonAsync<IssueModel, IssueModel>("rest/api/2/issue", issue, cancellationToken);
    //    //issue.ResetAllChanged();
    //    //res?.UpdateCustomFields(await GetCachedFieldsAsync());
    //    return res;
    //}

    public async Task<IssueModel?> CreateIssueAsync(CreateIssueModel createIssue, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(createIssue, nameof(createIssue));

        IssueModel? res = await PostAsJsonAsync<CreateIssueModel, IssueModel>("rest/api/2/issue", createIssue, cancellationToken);
        //issue.ResetAllChanged();
        //res?.UpdateCustomFields(await GetCachedFieldsAsync());
        return res;
    }

    //public async Task<IssueModel?> CreateIssueAsync(CreateIssueModel createIssue, CancellationToken cancellationToken = default)
    //{
    //    IssueModel? res = await PostAsJsonAsync<CreateIssueModel, IssueModel>("rest/api/2/issue", createIssue, cancellationToken);
    //    //issue.ResetAllChanged();
    //    //res?.UpdateCustomFields(await GetCachedFieldsAsync());
    //    return res;
    //}




    /// <summary>
    /// Returns a full representation of the issue for the given issue key. 
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="fields">Fields which should be filled.</param>
    /// <param name="expand">Objects which should be expanded.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueModel?> GetIssueAsync(string issueIdOrKey, string? fields, string? expand, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));


        string fieldsPar = string.IsNullOrEmpty(fields) ? "" : $"?fields={fields}";
        string expandPar = string.IsNullOrEmpty(expand) ? "" : (string.IsNullOrEmpty(fields) ? "?expand=" : "&expand=") + expand;
        try
        {
            //var res = await GetFromJsonAsync<IssueModel>($"rest/api/2/issue/{issueIdOrKey}{fieldsPar}{expandPar}", cancellationToken);
            var res = await GetFromJsonAsync<IssueModel>($"rest/api/2/issue/{issueIdOrKey}", cancellationToken);
            return res;
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
    /// Edits an issue.
    /// </summary>
    /// <param name="issue">Issue class to update.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task UpdateIssueAsync(Issue issue, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(issue, nameof(issue));

        //issue.UpdateCustomFields(await GetCachedFieldsAsync());
        //issue.SerializeMode = SerializeMode.Update; // set for trace
        //JsonTrace.WriteRequest(this, issue);
        //issue.SerializeMode = SerializeMode.Update; // set for update

        IssueModel req = new();
        await PutAsJsonAsync<IssueModel, IssueModel>($"rest/api/2/issue/{issue.Key}", req, cancellationToken);
        //issue.ResetAllChanged();
    }

    /// <summary>
    /// Delete an issue. If the issue has subtasks you must set the parameter deleteSubtasks=true to delete the issue. You cannot delete an issue without its subtasks also being deleted.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="deleteSubtasks">A String of true or false indicating that any subtasks should also be deleted. If the issue has no subtasks this parameter is ignored. If the issue has subtasks and this parameter is missing or false, then the issue will not be deleted and an error will be returned.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueAsync(string issueIdOrKey, bool deleteSubtasks = false, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        string delPar = deleteSubtasks ? "true" : "false";
        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}?deleteSubtasks={delPar}", cancellationToken);
    }

    /// <summary>
    /// Assigns an issue to a user. 
    /// You can use this resource to assign issues when the user submitting the request has the assign permission but not the edit issue permission. 
    /// If the user is <see cref="User.AutomaticAssignee">User.AutomaticAssignee</see> automatic assignee is used. <see cref="User.EmptyAssignee">User.EmptyAssignee</see> will remove the assignee.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="userName">User name to assign issue to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task AssignIssueAsync(string issueIdOrKey, string userName, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(userName, nameof(userName));

        var req = new AssignPutRequestModel() { Name = userName };
        await PutAsJsonAsync<AssignPutRequestModel, AssignPutRequestModel>($"rest/api/2/issue/{issueIdOrKey}/assignee", req, cancellationToken);
    }


    /*
     * 
     * https://gist.github.com/peteristhegreat/fbc1adae62ac1047761fc3aea496f9fd
    /rest/internal/2/issue/issue-key/clone

Post? 

-Body "{"summary":"CLONE - test","optionalFields":{},"includeAttachments":true}"

Not found in API documentation? catch with developer console.
    
    Scriptrunner script:
def issueResult = post('/rest/internal/2/issue/[issue key]/clone')
.header('Authorization', 'Basic [your base64 endcoded user:token ]')
.header("Content-Type", "application/json")
.body([summary : "CLONE - test"]).asString()
return issueResult
      
      
    */

    #endregion

    #region IssueLink

    /// <summary>
    /// Creates or updates a remote issue link. If a globalId is provided and a remote issue link exists with that globalId, the remote issue link is updated. Otherwise, the remote issue link is created.
    /// </summary>
    /// <param name="issueLink">IssueLink class to create.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task CreateIssueLinkAsync(IssueLinkModel issueLink, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(issueLink, nameof(issueLink));

        await PostAsJsonAsync<IssueLinkModel, IssueLinkModel>("rest/api/2/issueLink", issueLink, cancellationToken);
    }

    /// <summary>
    /// Get the remote issue link with the given id on the issue.
    /// </summary>
    /// <param name="issueLinkId">Id of the link.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueLinkModel?> GetIssueLinkAsync(string issueLinkId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueLinkId, nameof(issueLinkId));

        var res = await GetFromJsonAsync<IssueLinkModel>($"rest/api/2/issueLink/{issueLinkId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Delete the remote issue link with the given global id on the issue.
    /// </summary>
    /// <param name="linkId">Id of the link to delete.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueLinkAsync(string linkId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(linkId, nameof(linkId));

        await DeleteAsync($"rest/api/2/issueLink/{linkId}", cancellationToken);
    }

    #endregion

    #region IssueLinkType

    /// <summary>
    /// Returns a list of available issue link types, if issue linking is enabled. Each issue link type has an id, a name and a label for the outward and inward link relationship.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueLinkTypeModel>?> GetIssueLinkTypesAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<IssueLinkTypesRespnseModel>("rest/api/2/issueLinkType", cancellationToken);
        return res?.IssueLinkTypes;
    }

    /// <summary>
    /// Returns for a given issue link type id all information about this issue link type.
    /// </summary>
    /// <param name="issueLinkTypeId">Id of the link type.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueLinkTypeModel?> GetIssueLinkTypeAsync(string issueLinkTypeId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueLinkTypeId, nameof(issueLinkTypeId));

        var res = await GetFromJsonAsync<IssueLinkTypeModel>($"rest/api/2/issueLinkType/{issueLinkTypeId}", cancellationToken);
        return res;
    }

    #endregion

    #region IssueMeta

    /// <summary>
    /// Returns the meta data for creating issues. This includes the available projects, issue types and fields, 
    /// including field types and whether or not those fields are required. 
    /// Projects will not be returned if the user does not have permission to create issues in that project. 
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CreateMetaModel?> GetCreateMetaAsync(string project, string issueType, CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<CreateMetaModel>($"rest/api/2/issue/createmeta/{project}/issuetypes/{issueType}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns the meta data for editing an issue. 
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CreateMetaModel?> GetEditMetaAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<CreateMetaModel>($"rest/api/2/issue/{issueIdOrKey}/editmeta", cancellationToken);
        return res;
    }

    #endregion

    #region IssueNotify

    /// <summary>
    /// Sends a notification (email) to the list or recipients defined in the request.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="notify">Class with the notify nformations.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task<NotifyModel?> SendNotifyAsync(string issueIdOrKey, NotifyModel notify, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(notify, nameof(notify));

        var res = await PostAsJsonAsync<NotifyModel, NotifyModel>($"rest/api/2/issue/{issueIdOrKey}/notify", notify, cancellationToken);
        return res;

    }

    #endregion

    #region IssueRemoteLink

    /// <summary>
    /// A representing the remote issue links on the issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<RemoteLinkModel>?> GetIssueRemoteLinksAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<IEnumerable<RemoteLinkModel>>($"rest/api/2/issue/{issueIdOrKey}/remotelink", cancellationToken);
        return res;
    }

    //public async Task<RemoteLink> GetIssueRemoteLinkAsync(string issueIdOrKey, string globalId)
    //{
    //    IEnumerable<RemoteLink> res = await this.client.GetFromJsonAsync<IEnumerable<RemoteLink>>($"rest/api/2/issue/{issueIdOrKey}/remotelink?globalId={globalId}", this.options, cancellationToken);
    //    return res.ElementAtOrDefault(0);
    //}

    /// <summary>
    /// Creates or updates a remote issue link from a JSON representation.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="remoteLink">Class of the remote link to create.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>If a globalId is provided and a remote issue link exists with that globalId, the remote issue link is updated. Otherwise, the remote issue link is created.</remarks>
    public async Task<RemoteLinkModel?> CreateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLinkModel remoteLink, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(remoteLink, nameof(remoteLink));

        var res = await PostAsJsonAsync<RemoteLinkModel, RemoteLinkModel>($"rest/api/2/issue/{issueIdOrKey}/remotelink", remoteLink, cancellationToken);
        return res;
    }

    //public async Task DeleteIssueRemoteLinkAsync(string issueIdOrKey, string globalId, CancellationToken cancellationToken = default)
    //{
    //    using (HttpResponseMessage response = await this.client.DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/remotelink?globalId={globalId}", cancellationToken))
    //    {
    //        response.EnsureSuccess();
    //    }
    //}

    /// <summary>
    /// Get the remote issue link with the given id on the issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="remoteLinkId">Id of the remote link.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<RemoteLinkModel?> GetIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(remoteLinkId, nameof(remoteLinkId));

        var res = await GetFromJsonAsync<RemoteLinkModel>($"rest/api/2/issue/{issueIdOrKey}/remotelink/{remoteLinkId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Updates a remote issue link.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="remoteLink">Remote link to update.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<RemoteLinkModel?> UpdateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLinkModel remoteLink, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(remoteLink, nameof(remoteLink));

        var res = await PutAsJsonAsync<RemoteLinkModel, RemoteLinkModel>($"rest/api/2/issue/{issueIdOrKey}/remotelink/{remoteLink.Id}", remoteLink, cancellationToken);
        return res;
    }

    /// <summary>
    /// Delete the remote issue link with the given global id on the issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="remoteLinkId">Id of the remote link.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(remoteLinkId, nameof(remoteLinkId));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/remotelink/{remoteLinkId}", cancellationToken);
    }

    #endregion

    #region IssueTransition

    /// <summary>
    /// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types. 
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<TransitionModel>?> GetTransitionsAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<TransitionGetResultModel>($"rest/api/2/issue/{issueIdOrKey}/transitions", cancellationToken);
        return res?.Transitions;
    }

    /// <summary>
    /// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="transitionId">Id of the transition.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<TransitionModel?> GetTransitionAsync(string issueIdOrKey, string transitionId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(transitionId, nameof(transitionId));

        var res = await GetFromJsonAsync<TransitionGetResultModel>($"rest/api/2/issue/{issueIdOrKey}/transitions?transitionId={transitionId}", cancellationToken);
        return res?.Transitions?.FirstOrDefault();
    }

    /// <summary>
    /// Perform a transition on an issue. When performing the transition you can udate or set other issue fields. 
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="transition">Transition class.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<TransitionModel?> TransitionAsync(string issueIdOrKey, TransitionModel transition, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(transition, nameof(transition));

        var req = new TransitionPostReqModel() { Transition = transition };
        var res = await PostAsJsonAsync<TransitionPostReqModel, TransitionModel>($"rest/api/2/issue/{issueIdOrKey}/transitions", req, cancellationToken);
        return res;
    }

    #endregion

    #region IssueType

    /// <summary>
    /// Returns a list of all issue types visible to the user.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueTypeModel>?> GetIssueTypesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<IssueTypeModel>? res = await GetFromJsonAsync<IEnumerable<IssueTypeModel>>("rest/api/2/issuetype", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a full representation of the issue type that has the given id.
    /// </summary>
    /// <param name="issueTypeId">Id of the issue type.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueTypeModel?> GetIssueTypeAsync(string issueTypeId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueTypeId, nameof(issueTypeId));

        var res = await GetFromJsonAsync<IssueTypeModel>($"rest/api/2/issuetype/{issueTypeId}", cancellationToken);
        return res;
    }

    #endregion

    #region IssueVotes

    /// <summary>
    /// A resource representing the voters on the issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<VotesModel?> GetIssueVotesAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<VotesModel>($"rest/api/2/issue/{issueIdOrKey}/votes", cancellationToken);
        return res;
    }

    /// <summary>
    /// CastModel your vote in favour of an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task AddIssueVoteAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        await PostAsync($"rest/api/2/issue/{issueIdOrKey}/votes", cancellationToken);
    }

    /// <summary>
    /// Remove your vote from an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueVoteAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/votes", cancellationToken);
    }

    #endregion

    #region IssueWatcher

    /// <summary>
    /// Returns the list of watchers for the issue with the given key.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<WatchersModel?> GetIssueWatchersAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<WatchersModel>($"rest/api/2/issue/{issueIdOrKey}/watchers", cancellationToken);
        return res;
    }

    /// <summary>
    /// Adds a user to an issue's watcher list.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="username">Username of the new watcher.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task AddIssueWatcherAsync(string issueIdOrKey, string username, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(username, nameof(username));

        await PostAsJsonAsync<string, string>($"rest/api/2/issue/{issueIdOrKey}/watchers", username, cancellationToken);
    }

    /// <summary>
    /// Removes a user from an issue's watcher list.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="username">Username of the watcher to delete.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueWatcherAsync(string issueIdOrKey, string username, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(username, nameof(username));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/watchers?username={username}", cancellationToken);
    }

    #endregion

    #region Myself

    public async Task<UserModel?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<UserModel>("rest/api/2/myself", cancellationToken);
        return res;
    }

    #endregion

    #region Priority

    /// <summary>
    /// Returns a list of all issue priorities.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<PriorityModel>?> GetPrioritiesAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<IEnumerable<PriorityModel>>("rest/api/2/priority", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns an issue priority.
    /// </summary>
    /// <param name="priorityId">Id of the priority.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<PriorityModel?> GetPriorityAsync(string priorityId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(priorityId, nameof(priorityId));

        var res = await GetFromJsonAsync<PriorityModel>($"rest/api/2/priority/{priorityId}", cancellationToken);
        return res;
    }

    #endregion

    #region Project

    /// <summary>
    /// Returns all projects which are visible for the currently logged in user. If no user is logged in, it returns the list of projects that are visible when using anonymous access.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only the fields Self, Id, Key, Name and AvatarUrls will be filled by GetProjectsAsync. Call GetProjectAsync to get all fields. </remarks>
    public async Task<IEnumerable<ProjectModel>?> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<IEnumerable<ProjectModel>>("rest/api/2/project", cancellationToken);
        return res;
    }

    /// <summary>
    /// Contains a full representation of a project in JSON format.
    /// </summary>
    /// <param name="projectKey">Key of the project.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ProjectModel?> GetProjectAsync(string projectIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(projectIdOrKey, nameof(projectIdOrKey));

        var res = await GetFromJsonAsync<ProjectModel>($"rest/api/2/project/{projectIdOrKey}", cancellationToken);
        return res;
    }

    #endregion

    #region ProjectType

    #endregion

    #region Resolution

    /// <summary>
    /// Returns a list of all resolutions.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<ResolutionModel>?> GetResolutionsAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<IEnumerable<ResolutionModel>>("rest/api/2/resolution", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a resolution.
    /// </summary>
    /// <param name="resolutionId">Id of the resolution.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ResolutionModel?> GetResolutionAsync(string resolutionId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(resolutionId, nameof(resolutionId));

        var res = await GetFromJsonAsync<ResolutionModel>($"rest/api/2/resolution/{resolutionId}", cancellationToken);
        return res;
    }

    #endregion

    #region Search

    /// <summary>
    /// Performs a issue search using JQL.
    /// </summary>
    /// <param name="jql">JQL statement to search.</param>
    /// <param name="startAt">Start index.</param>
    /// <param name="maxResults">Maximum number of results.</param>
    /// <param name="fields">Fields to fill.</param>
    /// <param name="expand">Objects to expand.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<Issue>?> GetIssuesFromJqlAsync(string jql, int startAt = 0, int maxResults = 500, string? fields = null, string? expand = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(jql, nameof(jql));

        var req = new SearchRequestModel() { Jql = jql, StartAt = startAt, MaxResults = maxResults, Fields = fields, Expand = expand };
        try
        {
            var res = await PostAsJsonAsync<SearchRequestModel, SearchListModel>("rest/api/2/search", req, cancellationToken);

            //IEnumerable<FieldModel?>? fieldInfo = await GetCachedFieldsAsync();
            //foreach (Issue issue in res!.Issues)
            //{
            //    issue.UpdateCustomFields(fieldInfo!);
            //}
            return res?.Issues;
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

    #endregion

    #region ServerInfo

    /// <summary>
    /// Returns general information about the current JIRA server.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ServerInfoModel?> GetServerInfoAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<ServerInfoModel>("/rest/api/2/serverInfo", cancellationToken);
        return res;
    }

    #endregion

    #region Status

    /// <summary>
    /// Returns a list of all statuses.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<StatusModel>?> GetStatusesAsync(CancellationToken cancellationToken = default)
    {
        var res = await GetFromJsonAsync<IEnumerable<StatusModel>>("rest/api/2/status", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a full representation of the Status having the given id or name.
    /// </summary>
    /// <param name="statusIdOrName">Id or name of the status.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<StatusModel?> GetStatusAsync(string statusIdOrName, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(statusIdOrName, nameof(statusIdOrName));

        var res = await GetFromJsonAsync<StatusModel>($"rest/api/2/status/{statusIdOrName}", cancellationToken);
        return res;
    }

    #endregion

    #region User

    /// <summary>
    /// Returns a user.
    /// </summary>
    /// <param name="username">Name of the user.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>This resource cannot be accessed anonymously.</remarks>
    public async Task<UserModel?> GetUserAsync(string username, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(username, nameof(username));

        var res = await GetFromJsonAsync<UserModel>($"rest/api/2/user?username={username}", cancellationToken);
        return res;
    }

    #endregion

    #region Version

    /// <summary>
    /// Contains a full representation of a the specified project's versions.
    /// </summary>
    /// <param name="projectKey">Key of the project.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueVersion>?> GetVersionsAsync(string projectKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(projectKey, nameof(projectKey));

        IEnumerable<IssueVersion>? res = await GetFromJsonAsync<IEnumerable<IssueVersion>>("rest/api/2/project/{projectKey}/versions", cancellationToken);
        return res;
    }

    /// <summary>
    /// Create a version.
    /// </summary>
    /// <param name="version">Class of the version to create.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> CreateVersionAsync(IssueVersion version, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(version, nameof(version));

        IssueVersion? res = await PostAsJsonAsync<IssueVersion, IssueVersion>("rest/api/2/version", version, cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a project version.
    /// </summary>
    /// <param name="versionId">Id of the version.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> GetVersionAsync(string versionId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));

        IssueVersion? res = await GetFromJsonAsync<IssueVersion>("rest/api/2/version/{versionId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Modify a version.
    /// </summary>
    /// <param name="version">Class of the version to update.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> UpdateVersionAsync(IssueVersion version, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(version, nameof(version));

        IssueVersion? res = await PutAsJsonAsync<IssueVersion, IssueVersion>($"rest/api/2/version/{version.Id}", version, cancellationToken);
        return res;
    }

    /// <summary>
    /// Delete a project version.
    /// </summary>
    /// <param name="versionId">Id of the version to delete.</param>
    /// <param name="moveFixIssuesTo">Id of the version to move fix issues to.</param>
    /// <param name="moveAffectedIssuesTo">Id of the version to move affected issues to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteVersionAsync(string versionId, string? moveFixIssuesTo = null, string? moveAffectedIssuesTo = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));

        await DeleteAsync($"rest/api/2/version/{versionId}?{moveFixIssuesTo ?? string.Empty}{moveAffectedIssuesTo ?? string.Empty}", cancellationToken);
    }

    /// <summary>
    /// Modify a version's sequence within a project. 
    /// </summary>
    /// <param name="versionId">Id of the version to move.</param>
    /// <param name="versionIdAfter">Id of the version to move after.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> MoveVersionAsync(string versionId, string versionIdAfter, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));
        ArgumentNullException.ThrowIfNullOrEmpty(versionIdAfter, nameof(versionIdAfter));

        var req = new VersionMoveAfterPostRequestModel() { After = versionIdAfter };
        IssueVersion? res = await PostAsJsonAsync<VersionMoveAfterPostRequestModel, IssueVersion>($"rest/api/2/version/{versionId}/move", req, cancellationToken);
        return res;
    }

    /// <summary>
    /// Modify a version's sequence within a project. 
    /// </summary>
    /// <param name="versionId">Id of the version to move.</param>
    /// <param name="position">Position to move the version to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> MoveVersionAsync(string versionId, Position position, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));

        var req = new VersionMovePositionPostRequestModel() { Position = position };
        IssueVersion? res = await PostAsJsonAsync<VersionMovePositionPostRequestModel, IssueVersion>($"rest/api/2/version/{versionId}/move", req, cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a bean containing the number of fixed in and affected issues for the given version.
    /// </summary>
    /// <param name="versionId">Id of the version.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<int> VersionRelatedIssuesCountsAsync(string versionId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));

        var res = await GetFromJsonAsync<VersionRelatedIssueCountsModel>($"rest/api/2/version/{versionId}/relatedIssueCounts", cancellationToken);
        return res?.IssuesAffectedCount ?? 0;
    }

    /// <summary>
    /// Returns the number of unresolved issues for the given version.
    /// </summary>
    /// <param name="versionId">Id of the version.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<int> VersionUnresolvedIssueCountAsync(string versionId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));

        var res = await GetFromJsonAsync<VersionUnresolvedIssueCountModel>($"rest/api/2/version/{versionId}/unresolvedIssueCount", cancellationToken);
        return res?.IssuesUnresolvedCount ?? 0;
    }

    #endregion

    #region Workog

    /// <summary>
    /// Returns all work logs for an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<WorklogModel>?> GetIssueWorklogsAsync(string issueIdOrKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<WorklogListModel>($"rest/api/2/issue/{issueIdOrKey}/worklog", cancellationToken);
        return res?.Worklogs;
    }

    /// <summary>
    /// Adds a new worklog entry to an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="worklog">Worklog to add.</param>
    /// <param name="adjustEstimate">Adjust estimate flags.</param>
    /// <param name="value">Value for AdjustEstimate.New and AdjustEstimate.Manual.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task AddIssueWorklogAsync(string issueIdOrKey, WorklogModel worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string? value = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(worklog, nameof(worklog));

        string uri = adjustEstimate switch
        {
            AdjustEstimate.New => $"rest/api/2/issue/{issueIdOrKey}/worklog?adjustEstimate=new&newEstimate={value}",
            AdjustEstimate.Leave => $"rest/api/2/issue/{issueIdOrKey}/worklog?adjustEstimate=leave",
            AdjustEstimate.Manual => $"rest/api/2/issue/{issueIdOrKey}/worklog?adjustEstimate=manual&reduceBy={value}",
            _ => $"rest/api/2/issue/{issueIdOrKey}/worklog?adjustEstimate=auto"
        };

        await PostAsJsonAsync<WorklogModel, WorklogModel>(uri, worklog, cancellationToken);
    }

    /// <summary>
    /// Returns a specific worklog.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="worklogId">Id of the worklog.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<WorklogModel?> GetIssueWorklogAsync(string issueIdOrKey, string worklogId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(worklogId, nameof(worklogId));

        var res = await GetFromJsonAsync<WorklogModel>($"rest/api/2/issue/{issueIdOrKey}/worklog/{worklogId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Updates an existing worklog entry.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="worklog">Worklog class to update.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<WorklogModel?> UpdateIssueWorklogAsync(string issueIdOrKey, WorklogModel worklog, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(worklog, nameof(worklog));

        // must be set to 0; so sav
        int timeSpentSeconds = worklog.TimeSpentSeconds;
        worklog.TimeSpentSeconds = 0;

        var res = await PutAsJsonAsync<WorklogModel, WorklogModel>($"rest/api/2/issue/{issueIdOrKey}/worklog/{worklog.Id}", worklog, cancellationToken);
        worklog.TimeSpentSeconds = timeSpentSeconds;
        return res;
    }

    /// <summary>
    /// Deletes an existing worklog entry.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="worklogId">Id of the worklog to delete.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueWorklogAsync(string issueIdOrKey, string worklogId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(worklogId, nameof(worklogId));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/worklog/{worklogId}", cancellationToken);
    }

    #endregion
}
