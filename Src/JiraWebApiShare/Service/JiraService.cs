﻿namespace JiraWebApi.Service;

// https://developer.atlassian.com/server/jira/platform/rest/v10002/api-group-serverinfo/#api-group-serverinfo
// https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-projects/#api-group-projects

// https://docs.atlassian.com/software/jira/docs/api/REST/9.12.0/

internal class JiraService(Uri host, IAuthenticator? authenticator, string appName)
    : JsonService(host, authenticator, appName, SourceGenerationContext.Default)
{
    #region Private

    protected override string? AuthenticationTestUrl => "/rest/api/2/serverInfo";
   
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

    #endregion

    #region Attachments

    /// <summary>
    /// Returns the meta informations for an attachments, specifically if they are enabled and the maximum upload size allowed.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<AttachmentMetaModel?> GetAttachmentMetaAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<AttachmentMetaModel>("rest/api/2/attachment/meta", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns the meta-data for an attachment, including the URI of the actual attached file.
    /// </summary>
    /// <param name="attachmentId">The id of the attachment to get.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<AttachmentModel?> GetAttachmentAsync(string attachmentId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(attachmentId, nameof(attachmentId));

        var res = await GetFromJsonAsync<AttachmentModel>($"rest/api/2/attachment/{attachmentId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Remove an attachment from an issue.
    /// </summary>
    /// <param name="attachmentId">The id of the attachment to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteAttachmentAsync(string attachmentId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(attachmentId, nameof(attachmentId));

        await DeleteAsync($"rest/api/2/attachment/{attachmentId}", cancellationToken);
    }

    /// <summary>
    /// Add one or more attachments to an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="files">List with attachments to add.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<AttachmentModel>?> AddAttachmentsAsync(string issueIdOrKey, IEnumerable<KeyValuePair<string, Stream>> files, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<Stream> GetAttachmentStreamAsync(Uri attachmentUrl, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<CommentModel>?> GetCommentsAsync(string issueIdOrKey, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CommentModel?> AddCommentAsync(string issueIdOrKey, CommentModel comment, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CommentModel?> GetCommentAsync(string issueIdOrKey, string commentId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Json bug in JIRA 5.0.4</remarks>
    public async Task<CommentModel?> UpdateCommentAsync(string issueIdOrKey, CommentModel comment, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteCommentAsync(string issueIdOrKey, string commentId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(commentId, nameof(commentId));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/comment/{commentId}", cancellationToken);
    }

    #endregion

    #region Component

    /// <summary>
    /// Retrieves a list of all components for the specified project.
    /// </summary>
    /// <param name="projectIdOrKey">The ID or key of the project to retrieve components for.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of 
    /// <see cref="ComponentModel"/> objects representing the components of the project, or <c>null</c> if no components are found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="projectIdOrKey"/> is <c>null</c> or empty.
    /// </exception>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<FieldModel>?> GetFieldsAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<FieldModel>>("rest/api/2/field", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a full representation of the Custom Field Option that has the given id.
    /// </summary>
    /// <param name="customFieldOptionId">Id of the custom field option.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CustomFieldOptionModel?> GetCustomFieldOptionAsync(string customFieldOptionId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task<FilterModel?> CreateFilterAsync(FilterModel filter, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));

        var res = await PostAsJsonAsync<FilterModel, FilterModel>("rest/api/2/filter", filter, cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns the favourite filters of the logged-in user.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<FilterModel>?> GetFilterFavouritesAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<FilterModel>>("rest/api/2/filter/favourite", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a filter given an id.
    /// </summary>
    /// <param name="filterId">Id of the filter.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<FilterModel?> GetFilterAsync(string filterId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(filterId, nameof(filterId));

        var res = await GetFromJsonAsync<FilterModel>($"rest/api/2/filter/{filterId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Updates an existing filter, and returns its new value.
    /// </summary>
    /// <param name="filter">Filter class to update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task<FilterModel?> UpdateFilterAsync(FilterModel filter, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));

        var res = await PutAsJsonAsync<FilterModel, FilterModel>($"rest/api/2/filter/{filter.Id}", filter, cancellationToken);
        return res;
    }

    /// <summary>
    /// Delete a filter.
    /// </summary>
    /// <param name="filterId">Id of the filter to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task DeleteFilterAsync(string filterId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// Creates a new issue or sub-task in JIRA.
    /// </summary>
    /// <param name="createIssue">The <see cref="IssueModel"/> object containing the details of the issue to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created issue
    /// as an <see cref="IssueModel"/>, or <c>null</c> if the creation failed.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="createIssue"/> is <c>null</c>.
    /// </exception>
    public async Task<IssueModel?> CreateIssueAsync(IssueModel createIssue, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(createIssue, nameof(createIssue));

        IssueModel? res = await PostAsJsonAsync<IssueModel, IssueModel>("rest/api/2/issue", createIssue, cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a full representation of the issue for the given issue key. 
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="fields">Fields which should be filled.</param>
    /// <param name="expand">Objects which should be expanded.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task UpdateIssueAsync(Issue issue, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="userName">User name to assign issue to.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task AssignIssueAsync(string issueIdOrKey, string userName, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task CreateIssueLinkAsync(IssueLinkModel issueLink, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(issueLink, nameof(issueLink));

        await PostAsJsonAsync<IssueLinkModel, IssueLinkModel>("rest/api/2/issueLink", issueLink, cancellationToken);
    }

    /// <summary>
    /// Get the remote issue link with the given id on the issue.
    /// </summary>
    /// <param name="issueLinkId">Id of the link.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueLinkModel?> GetIssueLinkAsync(string issueLinkId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueLinkId, nameof(issueLinkId));

        var res = await GetFromJsonAsync<IssueLinkModel>($"rest/api/2/issueLink/{issueLinkId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Delete the remote issue link with the given global id on the issue.
    /// </summary>
    /// <param name="linkId">Id of the link to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueLinkAsync(string linkId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(linkId, nameof(linkId));

        await DeleteAsync($"rest/api/2/issueLink/{linkId}", cancellationToken);
    }

    #endregion

    #region IssueLinkType

    /// <summary>
    /// Returns a list of available issue link types, if issue linking is enabled. Each issue link type has an id, a name and a label for the outward and inward link relationship.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueLinkTypeModel>?> GetIssueLinkTypesAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IssueLinkTypesRespnseModel>("rest/api/2/issueLinkType", cancellationToken);
        return res?.IssueLinkTypes;
    }

    /// <summary>
    /// Returns for a given issue link type id all information about this issue link type.
    /// </summary>
    /// <param name="issueLinkTypeId">Id of the link type.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueLinkTypeModel?> GetIssueLinkTypeAsync(string issueLinkTypeId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueLinkTypeId, nameof(issueLinkTypeId));

        var res = await GetFromJsonAsync<IssueLinkTypeModel>($"rest/api/2/issueLinkType/{issueLinkTypeId}", cancellationToken);
        return res;
    }

    #endregion

    #region IssueMeta

   
    public async Task<CreateMetaModel?> GetCreateMetaAsync(string project, int issueType, CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<CreateMetaModel>($"rest/api/2/issue/createmeta/{project}/issuetypes/{issueType}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns the meta data for editing an issue. 
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<CreateMetaModel?> GetEditMetaAsync(string issueIdOrKey, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    public async Task<NotifyModel?> SendNotifyAsync(string issueIdOrKey, NotifyModel notify, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<RemoteLinkModel>?> GetIssueRemoteLinksAsync(string issueIdOrKey, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>If a globalId is provided and a remote issue link exists with that globalId, the remote issue link is updated. Otherwise, the remote issue link is created.</remarks>
    public async Task<RemoteLinkModel?> CreateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLinkModel remoteLink, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNull(remoteLink, nameof(remoteLink));

        var res = await PostAsJsonAsync<RemoteLinkModel, RemoteLinkModel>($"rest/api/2/issue/{issueIdOrKey}/remotelink", remoteLink, cancellationToken);
        return res;
    }

    //public async Task DeleteIssueRemoteLinkAsync(string issueIdOrKey, string globalId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<RemoteLinkModel?> GetIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<RemoteLinkModel?> UpdateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLinkModel remoteLink, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<TransitionModel>?> GetTransitionsAsync(string issueIdOrKey, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<TransitionModel?> GetTransitionAsync(string issueIdOrKey, string transitionId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<TransitionModel?> TransitionAsync(string issueIdOrKey, TransitionModel transition, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueTypeModel>?> GetIssueTypesAsync(CancellationToken cancellationToken)
    {
        IEnumerable<IssueTypeModel>? res = await GetFromJsonAsync<IEnumerable<IssueTypeModel>>("rest/api/2/issuetype", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a full representation of the issue type that has the given id.
    /// </summary>
    /// <param name="issueTypeId">Id of the issue type.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueTypeModel?> GetIssueTypeAsync(string issueTypeId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<VotesModel?> GetIssueVotesAsync(string issueIdOrKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        var res = await GetFromJsonAsync<VotesModel>($"rest/api/2/issue/{issueIdOrKey}/votes", cancellationToken);
        return res;
    }

    /// <summary>
    /// CastModel your vote in favour of an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task AddIssueVoteAsync(string issueIdOrKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));

        await PostAsync($"rest/api/2/issue/{issueIdOrKey}/votes", cancellationToken);
    }

    /// <summary>
    /// Remove your vote from an issue.
    /// </summary>
    /// <param name="issueIdOrKey">Id or key of the issue.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueVoteAsync(string issueIdOrKey, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<WatchersModel?> GetIssueWatchersAsync(string issueIdOrKey, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task AddIssueWatcherAsync(string issueIdOrKey, string username, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueWatcherAsync(string issueIdOrKey, string username, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(username, nameof(username));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/watchers?username={username}", cancellationToken);
    }

    #endregion

    #region Myself

    /// <summary>
    /// Retrieves information about the currently authenticated user.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the current user
    /// as a <see cref="UserModel"/>, or <c>null</c> if the user is not authenticated.
    /// </returns>
    /// <remarks>
    /// This method uses the JIRA REST API to fetch details about the currently logged-in user.
    /// </remarks>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<PriorityModel>?> GetPrioritiesAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<PriorityModel>>("rest/api/2/priority", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns an issue priority.
    /// </summary>
    /// <param name="priorityId">Id of the priority.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<PriorityModel?> GetPriorityAsync(int priorityId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(priorityId, nameof(priorityId));

        var res = await GetFromJsonAsync<PriorityModel>($"rest/api/2/priority/{priorityId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Retrieves a paginated list of issue priorities from the JIRA server.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// An asynchronous enumerable of <see cref="PriorityModel"/> objects representing the issue priorities.
    /// </returns>
    /// <remarks>
    /// This method fetches priorities in pages, iterating through all available pages until all priorities are retrieved.
    /// Each page contains a subset of the total priorities, determined by the server's pagination settings.
    /// </remarks>
    public async IAsyncEnumerable<PriorityModel> GetPrioritiesPagedAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
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
                yield return item;
            }
            total = res.Total;
            startAt = res.Values.Count();
        }
    }

    #endregion

    #region Project

    /// <summary>
    /// Returns all projects which are visible for the currently logged in user. If no user is logged in, it returns the list of projects that are visible when using anonymous access.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <remarks>Only the fields Self, Id, Key, Name and AvatarUrls will be filled by GetProjectsAsync. Call GetProjectAsync to get all fields. </remarks>
    public async Task<IEnumerable<ProjectModel>?> GetProjectsAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<ProjectModel>>("rest/api/2/project", cancellationToken);
        return res;
    }

    /// <summary>
    /// Retrieves a full representation of a project in JSON format.
    /// </summary>
    /// <param name="projectIdOrKey">The ID or key of the project to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the project as a 
    /// <see cref="ProjectModel"/>, or <c>null</c> if the project is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="projectIdOrKey"/> is <c>null</c> or empty.
    /// </exception>
    public async Task<ProjectModel?> GetProjectAsync(string projectIdOrKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(projectIdOrKey, nameof(projectIdOrKey));

        var res = await GetFromJsonAsync<ProjectModel>($"rest/api/2/project/{projectIdOrKey}", cancellationToken);
        return res;
    }

    #endregion

    #region ProjectType

    public async Task<IEnumerable<ProjectTypeModel>?> GetProjectTypesAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<ProjectTypeModel>>("rest/api/2/project/type", cancellationToken);
        return res;
    }

    public async Task<ProjectTypeModel?> GetProjectTypeAsync(string projectTypeKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(projectTypeKey, nameof(projectTypeKey));

        var res = await GetFromJsonAsync<ProjectTypeModel>($"rest/api/2/project/type/{projectTypeKey}", cancellationToken);
        return res;
    }

    public async Task<ProjectTypeModel?> GetAccessibleProjectTypeAsync(string projectTypeKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(projectTypeKey, nameof(projectTypeKey));

        var res = await GetFromJsonAsync<ProjectTypeModel>($"rest/api/2/project/type/{projectTypeKey}/accessible", cancellationToken);
        return res;
    }

    #endregion

    #region Resolution

    /// <summary>
    /// Returns a list of all resolutions.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<ResolutionModel>?> GetResolutionsAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<ResolutionModel>>("rest/api/2/resolution", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a resolution.
    /// </summary>
    /// <param name="resolutionId">Id of the resolution.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<ResolutionModel?> GetResolutionAsync(int resolutionId, CancellationToken cancellationToken)
    {
        //ArgumentNullException.ThrowIfNullOrEmpty(resolutionId, nameof(resolutionId));

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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<StatusModel>?> GetStatusesAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<StatusModel>>("rest/api/2/status", cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a full representation of the Status having the given id or name.
    /// </summary>
    /// <param name="statusIdOrName">Id or name of the status.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<StatusModel?> GetStatusAsync(string statusIdOrName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(statusIdOrName, nameof(statusIdOrName));

        var res = await GetFromJsonAsync<StatusModel>($"rest/api/2/status/{statusIdOrName}", cancellationToken);
        return res;
    }

    #endregion

    #region StatusCategory

    /// <summary>
    /// Returns a list of all statuses.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<StatusCategoryModel>?> GetStatusCategoriesAsync(CancellationToken cancellationToken)
    {
        var res = await GetFromJsonAsync<IEnumerable<StatusCategoryModel>>("rest/api/2/statuscategory", cancellationToken);
        return res;
    }

    /// <summary>
    /// Retrieves a full representation of a status category by its ID or name.
    /// </summary>
    /// <param name="statusCategoryIdOrName">The ID or name of the status category to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the status category
    /// as a <see cref="StatusCategoryModel"/>, or <c>null</c> if the status category is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="statusCategoryIdOrName"/> is <c>null</c> or empty.
    /// </exception>
    public async Task<StatusCategoryModel?> GetStatusCategoryAsync(string statusCategoryIdOrName, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(statusCategoryIdOrName, nameof(statusCategoryIdOrName));

        var res = await GetFromJsonAsync<StatusCategoryModel>($"rest/api/2/statuscategory/{statusCategoryIdOrName}", cancellationToken);
        return res;
    }

    #endregion

    #region User

    /// <summary>
    /// Returns a user.
    /// </summary>
    /// <param name="username">Name of the user.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<IssueVersion>?> GetVersionsAsync(string projectKey, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(projectKey, nameof(projectKey));

        IEnumerable<IssueVersion>? res = await GetFromJsonAsync<IEnumerable<IssueVersion>>("rest/api/2/project/{projectKey}/versions", cancellationToken);
        return res;
    }

    /// <summary>
    /// Create a version.
    /// </summary>
    /// <param name="version">Class of the version to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> CreateVersionAsync(IssueVersion version, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(version, nameof(version));

        IssueVersion? res = await PostAsJsonAsync<IssueVersion, IssueVersion>("rest/api/2/version", version, cancellationToken);
        return res;
    }

    /// <summary>
    /// Returns a project version.
    /// </summary>
    /// <param name="versionId">Id of the version.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> GetVersionAsync(string versionId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));

        IssueVersion? res = await GetFromJsonAsync<IssueVersion>("rest/api/2/version/{versionId}", cancellationToken);
        return res;
    }

    /// <summary>
    /// Modify a version.
    /// </summary>
    /// <param name="version">Class of the version to update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> UpdateVersionAsync(IssueVersion version, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> MoveVersionAsync(string versionId, string versionIdAfter, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IssueVersion?> MoveVersionAsync(string versionId, Position position, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<int> VersionRelatedIssuesCountsAsync(string versionId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(versionId, nameof(versionId));

        var res = await GetFromJsonAsync<VersionRelatedIssueCountsModel>($"rest/api/2/version/{versionId}/relatedIssueCounts", cancellationToken);
        return res?.IssuesAffectedCount ?? 0;
    }

    /// <summary>
    /// Returns the number of unresolved issues for the given version.
    /// </summary>
    /// <param name="versionId">Id of the version.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<int> VersionUnresolvedIssueCountAsync(string versionId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<IEnumerable<WorklogModel>?> GetIssueWorklogsAsync(string issueIdOrKey, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<WorklogModel?> GetIssueWorklogAsync(string issueIdOrKey, string worklogId, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<WorklogModel?> UpdateIssueWorklogAsync(string issueIdOrKey, WorklogModel worklog, CancellationToken cancellationToken)
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
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task DeleteIssueWorklogAsync(string issueIdOrKey, string worklogId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
        ArgumentNullException.ThrowIfNullOrEmpty(worklogId, nameof(worklogId));

        await DeleteAsync($"rest/api/2/issue/{issueIdOrKey}/worklog/{worklogId}", cancellationToken);
    }

    #endregion
}
