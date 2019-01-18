using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JiraWebApi
{
    public interface IJira
    {
        #region Login

        /// <summary>
        /// Creates a new session for a user in JIRA.
        /// </summary>
        /// <param name="username">Name of the user to login.</param>
        /// <param name="password">Password of the user to login.</param>
        void Login(string username, string password);

        /// <summary>
        /// Creates a new session for a user in JIRA.
        /// </summary>
        /// <param name="username">Name of the user to login.</param>
        /// <param name="password">Password of the user to login.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task LoginAsync(string username, string password);

        /// <summary>
        /// Logs the current user out of JIRA, destroying the existing session, if any.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task LogoutAsync();

        /// <summary>
        /// Returns information about the currently authenticated user's session.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<LoginInfo> GetLoginInfoAsync();

        #endregion

        #region ServerInfo

        /// <summary>
        /// Returns general information about the current JIRA server.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<ServerInfo> GetServerInfoAsync();

        #endregion

        #region IssueType

        /// <summary>
        /// Returns a list of all issue types visible to the user.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<IssueType>> GetIssueTypesAsync();

        /// <summary>
        /// Create a issue type.
        /// </summary>
        /// <param name="issueType">IssueType class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueTypeBase> CreateIssueTypeAsync(IssueTypeBase issueType);

        /// <summary>
        /// Returns a full representation of the issue type that has the given id.
        /// </summary>
        /// <param name="issueTypeId">Id of the issue type.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueType> GetIssueTypeAsync(string issueTypeId);

        #endregion

        #region Priority

        /// <summary>
        /// Returns a list of all issue priorities.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Priority>> GetPrioritiesAsync();

        /// <summary>
        /// Returns an issue priority.
        /// </summary>
        /// <param name="priorityId">Id of the priority.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Priority> GetPriorityAsync(string priorityId);

        #endregion

        #region Resolution

        /// <summary>
        /// Returns a list of all resolutions.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Resolution>> GetResolutionsAsync();

        /// <summary>
        /// Returns a resolution.
        /// </summary>
        /// <param name="resolutionId">Id of the resolution.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Resolution> GetResolutionAsync(string resolutionId);

        #endregion

        #region Status

        /// <summary>
        /// Returns a list of all statuses.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Status>> GetStatusesAsync();

        /// <summary>
        /// Returns a full representation of the Status having the given id or name.
        /// </summary>
        /// <param name="statusIdOrName">Id or name of the status.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Status> GetStatusAsync(string statusIdOrName);

        #endregion

        #region IssueLinkType

        /// <summary>
        /// Returns a list of available issue link types, if issue linking is enabled. Each issue link type has an id, a name and a label for the outward and inward link relationship.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<IssueLinkType>> GetIssueLinkTypesAsync();

        /// <summary>
        /// Create a issue link type.
        /// </summary>
        /// <param name="issueLinkType">IssueLinkType class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueLinkTypeBase> CreateIssueLinkTypeAsync(IssueLinkTypeBase issueLinkType);

        /// <summary>
        /// Returns for a given issue link type id all information about this issue link type.
        /// </summary>
        /// <param name="issueLinkTypeId">Id of the link type.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueLinkType> GetIssueLinkTypeAsync(string issueLinkTypeId);

        #endregion

        #region Field

        /// <summary>
        /// Returns a list of all fields, both System and Custom.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Field>> GetFieldsAsync();

        /// <summary>
        /// Create a custom field.
        /// </summary>
        /// <param name="customField">CustomField class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<CustomFieldBase> CreateCustomFieldAsync(CustomFieldBase customField);

        /// <summary>
        /// Returns a full representation of the Custom Field Option that has the given id.
        /// </summary>
        /// <param name="customFieldOptionId">Id of the custom field option.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<CustomFieldOption> GetCustomFieldOptionAsync(string customFieldOptionId);

        #endregion

        #region Group

        /// <summary>
        /// Get group with specified group name.
        /// </summary>
        /// <param name="groupName">Name of the group</param>
        /// <param name="expandGroup">Expand group parameter.</param>
        /// <returns>Jira group.</returns>
        /// <remarks>Only supported with JIRA 6.0 or later</remarks>
        Task<Group> GetGroupAsync(string groupName, string expandGroup = null);

        #endregion

        #region User

        /// <summary>
        /// Returns a user.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>This resource cannot be accessed anonymously.</remarks>
        Task<User> GetUserAsync(string username);

        #endregion

        #region Filter

        /// <summary>
        /// Creates a new filter, and returns newly created filter Currently sets permissions just using the users default sharing permissions.
        /// </summary>
        /// <param name="filter">Filter class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        Task<Filter> CreateFilterAsync(Filter filter);

        /// <summary>
        /// Returns the favourite filters of the logged-in user.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Filter>> GetFilterFavouritesAsync();

        /// <summary>
        /// Returns a filter given an id.
        /// </summary>
        /// <param name="filterId">Id of the filter.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Filter> GetFilterAsync(string filterId);

        /// <summary>
        /// Updates an existing filter, and returns its new value.
        /// </summary>
        /// <param name="filter">Filter class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        Task<Filter> UpdateFilterAsync(Filter filter);

        /// <summary>
        /// Delete a filter.
        /// </summary>
        /// <param name="filterId">Id of the filter to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        Task DeleteFilterAsync(string filterId);

        #endregion

        #region Component

        /// <summary>
        /// Contains a full representation of a the specified project's components.
        /// </summary>
        /// <param name="projectKey">Key of the project.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Component>> GetComponentsAsync(string projectKey);

        /// <summary>
        /// Create a component.
        /// </summary>
        /// <param name="component">Component class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Component> CreateComponentAsync(Component component);

        /// <summary>
        /// Returns a project component.
        /// </summary>
        /// <param name="componentId">Id of the component to get.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Component> GetComponentAsync(string componentId);

        /// <summary>
        /// Modify a component.
        /// </summary>
        /// <param name="component">Component class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Component> UpdateComponentAsync(Component component);

        /// <summary>
        /// Delete a project component.
        /// </summary>
        /// <param name="componentId">Id of the component to delete.</param>
        /// <param name="moveIssuesTo">The new component applied to issues whose 'id' component will be deleted. If this value is null, then the 'id' component is simply removed from the related isues.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteComponentAsync(string componentId, string moveIssuesTo = null);

        /// <summary>
        /// Returns counts of issues related to this component.
        /// </summary>
        /// <param name="componentId">Id of the component.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<int> ComponentRelatedIssuesCountAsync(string componentId);

        #endregion

        #region Version

        /// <summary>
        /// Contains a full representation of a the specified project's versions.
        /// </summary>
        /// <param name="projectKey">Key of the project.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<IssueVersion>> GetVersionsAsync(string projectKey);

        /// <summary>
        /// Create a version.
        /// </summary>
        /// <param name="version">Class of the version to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueVersion> CreateVersionAsync(IssueVersion version);

        /// <summary>
        /// Returns a project version.
        /// </summary>
        /// <param name="versionId">Id of the version.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueVersion> GetVersionAsync(string versionId);

        /// <summary>
        /// Modify a version.
        /// </summary>
        /// <param name="version">Class of the version to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueVersion> UpdateVersionAsync(IssueVersion version);

        /// <summary>
        /// Delete a project version.
        /// </summary>
        /// <param name="versionId">Id of the version to delete.</param>
        /// <param name="moveFixIssuesTo">Id of the version to move fix issues to.</param>
        /// <param name="moveAffectedIssuesTo">Id of the version to move affected issues to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteVersionAsync(string versionId, string moveFixIssuesTo = null, string moveAffectedIssuesTo = null);

        /// <summary>
        /// Modify a version's sequence within a project. 
        /// </summary>
        /// <param name="versionId">Id of the version to move.</param>
        /// <param name="versionIdAfter">Id of the version to move after.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueVersion> MoveVersionAsync(string versionId, string versionIdAfter);

        /// <summary>
        /// Modify a version's sequence within a project. 
        /// </summary>
        /// <param name="versionId">Id of the version to move.</param>
        /// <param name="position">Position to move the version to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueVersion> MoveVersionAsync(string versionId, Position position);

        /// <summary>
        /// Returns a bean containing the number of fixed in and affected issues for the given version.
        /// </summary>
        /// <param name="versionId">Id of the version.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<int> VersionRelatedIssuesCountsAsync(string versionId);

        /// <summary>
        /// Returns the number of unresolved issues for the given version.
        /// </summary>
        /// <param name="versionId">Id of the version.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<int> VersionUnresolvedIssueCountAsync(string versionId);

        #endregion

        #region Project

        /// <summary>
        /// Returns all projects which are visible for the currently logged in user. If no user is logged in, it returns the list of projects that are visible when using anonymous access.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only the fields Self, Id, Key, Name and AvatarUrls will be filled by GetProjectsAsync. Call GetProjectto get all fields. </remarks>
        Task<IEnumerable<Project>> GetProjectsAsync();

        /// <summary>
        /// Contains a full representation of a project in JSON format.
        /// </summary>
        /// <param name="projectKey">Key of the project.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Project> GetProjectAsync(string projectKey);

        #endregion

        #region Comment

        /// <summary>
        /// Returns all comments for an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Comment>> GetCommentsAsync(string issueIdOrKey);

        /// <summary>
        /// Adds a new comment to an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="comment">Comment class to add.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Comment> AddCommentAsync(string issueIdOrKey, Comment comment);

        /// <summary>
        /// Returns all comments for an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="commentId">Id of the comment.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Comment> GetCommentAsync(string issueIdOrKey, string commentId);

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="comment">Class of the comment to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Json bug in JIRA 5.0.4</remarks>
        Task<Comment> UpdateCommentAsync(string issueIdOrKey, Comment comment);

        /// <summary>
        /// Deletes an existing comment.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="commentId">Id of the comment.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteCommentAsync(string issueIdOrKey, string commentId);

        #endregion

        #region Meta

        /// <summary>
        /// Returns the meta data for creating issues. This includes the available projects, issue types and fields, 
        /// including field types and whether or not those fields are required. 
        /// Projects will not be returned if the user does not have permission to create issues in that project. 
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<CreateMeta> GetCreateMetaAsync(/*string projectKey, string issueTypeId*/);

        /// <summary>
        /// Returns the meta data for editing an issue. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<EditMeta> GetEditMetaAsync(string issueIdOrKey);

        #endregion

        #region Notify

        /// <summary>
        /// Sends a notification (email) to the list or recipients defined in the request.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="notify">Class with the notify nformations.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        Task SendNotifyAsync(string issueIdOrKey, Notify notify);

        #endregion

        #region RemoteLink

        /// <summary>
        /// A representing the remote issue links on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<RemoteLink>> GetIssueRemoteLinksAsync(string issueIdOrKey);

        /// <summary>
        /// Creates or updates a remote issue link from a JSON representation.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLink">Class of the remote link to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>If a globalId is provided and a remote issue link exists with that globalId, the remote issue link is updated. Otherwise, the remote issue link is created.</remarks>
        Task<RemoteLink> CreateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLink remoteLink);

        /// <summary>
        /// Get the remote issue link with the given id on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLinkId">Id of the remote link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<RemoteLink> GetIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId);

        /// <summary>
        /// Updates a remote issue link.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLink">Remote link to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task UpdateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLink remoteLink);

        /// <summary>
        /// Delete the remote issue link with the given global id on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLinkId">Id of the remote link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId);

        #endregion

        #region Transition

        /// <summary>
        /// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Transition>> GetTransitionsAsync(string issueIdOrKey);

        /// <summary>
        /// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="transitionId">Id of the transition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Transition> GetTransitionAsync(string issueIdOrKey, string transitionId);

        /// <summary>
        /// Perform a transition on an issue. When performing the transition you can update or set other issue fields. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="transition">Transition class.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task TransitionAsync(string issueIdOrKey, Transition transition);

        #endregion

        #region Votes

        /// <summary>
        /// A resource representing the voters on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Votes> GetIssueVotesAsync(string issueIdOrKey);

        /// <summary>
        /// Cast your vote in favour of an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task AddIssueVoteAsync(string issueIdOrKey);

        /// <summary>
        /// Remove your vote from an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteIssueVoteAsync(string issueIdOrKey);

        #endregion

        #region Watchers

        /// <summary>
        /// Returns the list of watchers for the issue with the given key.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Watchers> GetIssueWatchersAsync(string issueIdOrKey);

        /// <summary>
        /// Adds a user to an issue's watcher list.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="username">Username of the new watcher.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task AddIssueWatcherAsync(string issueIdOrKey, string username);

        /// <summary>
        /// Removes a user from an issue's watcher list.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="username">Username of the watcher to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteIssueWatcherAsync(string issueIdOrKey, string username);

        #endregion

        #region Workog

        /// <summary>
        /// Returns all work logs for an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Worklog>> GetIssueWorklogsAsync(string issueIdOrKey);

        /// <summary>
        /// Adds a new worklog entry to an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklog">Worklog to add.</param>
        /// <param name="adjustEstimate">Adjust estimate flags.</param>
        /// <param name="value">Value for AdjustEstimate.New and AdjustEstimate.Manual.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task AddIssueWorklogAsync(string issueIdOrKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string value = null);

        /// <summary>
        /// Returns a specific worklog.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklogId">Id of the worklog.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Worklog> GetIssueWorklogAsync(string issueIdOrKey, string worklogId);

        /// <summary>
        /// Updates an existing worklog entry.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklog">Worklog class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Worklog> UpdateIssueWorklogAsync(string issueIdOrKey, Worklog worklog);

        /// <summary>
        /// Deletes an existing worklog entry.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklogId">Id of the worklog to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteIssueWorklogAsync(string issueIdOrKey, string worklogId);

        #endregion

        #region Attachments

        /// <summary>
        /// Returns the meta informations for an attachments, specifically if they are enabled and the maximum upload size allowed.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<AttachmentMeta> GetAttachmentMetaAsync();

        /// <summary>
        /// Returns the meta-data for an attachment, including the URI of the actual attached file.
        /// </summary>
        /// <param name="attachmentId">The id of the attachment to get.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Attachment> GetAttachmentAsync(string attachmentId);

        /// <summary>
        /// Remove an attachment from an issue.
        /// </summary>
        /// <param name="attachmentId">The id of the attachment to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteAttachmentAsync(string attachmentId);

        /// <summary>
        /// Add one or more attachments to an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="files">List with attachments to add.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IEnumerable<Attachment>> AddAttachmentsAsync(string issueIdOrKey, IEnumerable<KeyValuePair<string, Stream>> files);

        /// <summary>
        /// Get the date stream of an attachment.
        /// </summary>
        /// <param name="attachmentUrl">Url of the attachment.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Stream> GetAttachmentStreamAsync(Uri attachmentUrl);

        #endregion

        #region Link

        /// <summary>
        /// Creates or updates a remote issue link. If a globalId is provided and a remote issue link exists with that globalId, the remote issue link is updated. Otherwise, the remote issue link is created.
        /// </summary>
        /// <param name="issueLink">IssueLink class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task CreateIssueLinkAsync(IssueLink issueLink);

        /// <summary>
        /// Get the remote issue link with the given id on the issue.
        /// </summary>
        /// <param name="issueLinkId">Id of the link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<IssueLink> GetIssueLinkAsync(string issueLinkId);

        /// <summary>
        /// Delete the remote issue link with the given global id on the issue.
        /// </summary>
        /// <param name="linkId">Id of the link to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteIssueLinkAsync(string linkId);

        #endregion

        #region Issues

        /// <summary>
        /// Creates an issue or a sub-task.
        /// </summary>
        /// <param name="issue">Issue class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Issue> CreateIssueAsync(Issue issue);

        /// <summary>
        /// Returns a full representation of the issue for the given issue key. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="fields">Fields which should be filled.</param>
        /// <param name="expand">Objects which should be expanded.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<Issue> GetIssueAsync(string issueIdOrKey, string fields = null, string expand = null);

        /// <summary>
        /// Edits an issue.
        /// </summary>
        /// <param name="issue">Issue class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task UpdateIssueAsync(Issue issue);

        /// <summary>
        /// Delete an issue. If the issue has subtasks you must set the parameter deleteSubtasks=true to delete the issue. You cannot delete an issue without its subtasks also being deleted.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="deleteSubtasks">A String of true or false indicating that any subtasks should also be deleted. If the issue has no subtasks this parameter is ignored. If the issue has subtasks and this parameter is missing or false, then the issue will not be deleted and an error will be returned.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task DeleteIssueAsync(string issueIdOrKey, bool deleteSubtasks = false);

        /// <summary>
        /// Assigns an issue to a user. 
        /// You can use this resource to assign issues when the user submitting the request has the assign permission but not the edit issue permission. 
        /// If the user is <see cref="User.AutomaticAssignee">User.AutomaticAssignee</see> automatic assignee is used. <see cref="User.EmptyAssignee">User.EmptyAssignee</see> will remove the assignee.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="userName">User name to assign issue to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task AssignIssueAsync(string issueIdOrKey, string userName);

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
        Task<IEnumerable<Issue>> GetIssuesFromJqlAsync(string jql, int startAt = 0, int maxResults = 500, string fields = null, string expand = null);

        #endregion

        #region Linq

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
        IOrderedQueryable<Issue> Issues { get; }

        #endregion
    }
}
