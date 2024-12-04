namespace JiraWebApi;

/// <summary>
/// Provides a set of static (Shared in Visual Basic) methods for easier JIRA access. 
/// </summary>
public static class JiraExtentions
{
    /// <summary>
    /// Create a JIRA issue.
    /// </summary>
    /// <param name="jira">The Jira class.</param>
    /// <param name="type">Name of the issue type.</param>
    /// <param name="project">Name of the JIRA project.</param>
    /// <param name="summary">Summary of the new issue.</param>
    /// <param name="description">Description of the new issue.</param>
    /// <returns>The new issue embedded in the task object representing the asynchronous operation..</returns>
    public static async Task<Issue?> CreateIssueAsync(this Jira jira, string type, string project, string summary, string? description = null)
    {
        IEnumerable<IssueType>? issueTypes = await jira.GetIssueTypesAsync();
        IssueType? issueType = issueTypes!.SingleOrDefault(t => t.Name == type);
        IEnumerable<Project>? projects = await jira.GetProjectsAsync();
        Project? proj = projects!.SingleOrDefault(p => p.Name == project || p.Key == project);
        
        ArgumentNullException.ThrowIfNull(issueType, nameof(type));
        ArgumentNullException.ThrowIfNull(proj, nameof(project));

        var meta = await jira.GetCreateMetaAsync(proj.Key!, issueType.Id!);

        //Reporter
        //Summary
        //Project
        //    issuetype



        var issue = new Issue
        {
            IssueType = issueType,
            Project = proj,
            Summary = summary,
            Description = description
        };
        return await jira.CreateIssueAsync(issue);
    }

    /// <summary>
    /// Create a JIRA issue.
    /// </summary>
    /// <param name="jira">The Jira class.</param>
    /// <param name="parent">Key of the parent issue.</param>
    /// <param name="type">Name of the issue type.</param>
    /// <param name="project">Name of the JIRA project.</param>
    /// <param name="summary">Summary of the new issue.</param>
    /// <param name="description">Description of the new issue.</param>
    /// <returns>The new issue embedded in the task object representing the asynchronous operation..</returns>
    public static async Task<Issue?> CreateSubIssueAsync(this Jira jira, string parent, string type, string project, string summary, string? description = null)
    {
        var issueTypes = await jira.GetIssueTypesAsync();
        var projects = await jira.GetProjectsAsync();
        var issue = new Issue
        {
            IssueType = issueTypes!.Single(t => t.Name == type),
            Project = projects!.Single(p => p.Name == project),
            Summary = summary,
            Description = description,
            Parent = new Issue(parent)
        };
        return await jira.CreateIssueAsync(issue);
    }

    ///// <summary>
    ///// Create an issue link.
    ///// </summary>
    ///// <param name="jira">The Jira class.</param>
    ///// <param name="issueLinkTypeName">Name of the link type.</param>
    ///// <param name="inwardIssueKey">Key of the inward issue.</param>
    ///// <param name="outwardIssueKey">Key of the outward issue.</param>
    ///// <param name="comment">Comment to add.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public static async Task CreateIssueLinkAsync(this Jira jira, string issueLinkTypeName, string inwardIssueKey, string outwardIssueKey, string? comment = null)
    //{
    //    IEnumerable<IssueLinkType>? issueLinkTypes = await jira.GetIssueLinkTypesAsync();
    //    IssueLinkType? issueLinkType = issueLinkTypes?.Where(t => t.Name == issueLinkTypeName).FirstOrDefault();

    //    //IssueLink link = new IssueLink();
    //    //link.Type = issueLinkTypes.Where(t => t.Name == issueLinkTypeName).FirstOrDefault();
    //    //link.Comment = string.IsNullOrEmpty(comment) ? null : new Comment() { Body = "comment" };
    //    //link.InwardIssue = new Issue(inwardIssueKey);
    //    //link.OutwardIssue = new Issue(outwardIssueKey);
    //    //await jira.CreateIssueLinkAsync(link);

    //    await jira.CreateIssueLinkAsync(issueLinkType!, inwardIssueKey, outwardIssueKey, comment);
    //}

    /// <summary>
    /// Create an issue link.
    /// </summary>
    /// <param name="jira">The Jira class.</param>
    /// <param name="issueLinkType">Issue link type.</param>
    /// <param name="inwardIssueKey">Issue key of the inward issue.</param>
    /// <param name="outwardIssueKey">Issue key of the outward issue.</param>
    /// <param name="comment">A comment created with the link.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    //public static async Task CreateIssueLinkAsync(this Jira jira, IssueLinkType issueLinkType, string inwardIssueKey, string outwardIssueKey, string? comment = null)
    //{
    //    //IssueLink link = new IssueLink();
    //    //link.Type = issueLinkType;
    //    //link.Comment = string.IsNullOrEmpty(comment) ? null : new Comment() { Body = "comment" };
    //    //link.InwardIssue = new Issue(inwardIssueKey);
    //    //link.OutwardIssue = new Issue(outwardIssueKey);
    //    //await jira.CreateIssueLinkAsync(link);
    //    await jira.CreateIssueLinkAsync(issueLinkType, new Issue(inwardIssueKey), new Issue(outwardIssueKey), comment);
    //}

    ///// <summary>
    ///// Create an issue link
    ///// </summary>
    ///// <param name="jira">The Jira class.</param>
    ///// <param name="issueLinkType">Issue link type.</param>
    ///// <param name="inwardIssue">The inward issue.</param>
    ///// <param name="outwardIssue">Teh outward issue.</param>
    ///// <param name="comment">A comment created with the link.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public static async Task CreateIssueLinkAsync(this Jira jira, IssueLinkType issueLinkType, Issue inwardIssue, Issue outwardIssue, string? comment = null)
    //{
    //    var link = new IssueLink();
    //    link.Type = issueLinkType;
    //    link.Comment = string.IsNullOrEmpty(comment) ? null : new Comment() { Body = "comment" };
    //    link.InwardIssue = inwardIssue;
    //    link.OutwardIssue = outwardIssue;
    //    await jira.CreateIssueLinkAsync(link);
    //}

    ///// <summary>
    ///// Creates a new filter, and returns newly created filter Currently sets permissions just using the users default sharing permissions.
    ///// </summary>
    ///// <param name="jira">The Jira class.</param>
    ///// <param name="name">Name of the filter to create.</param>
    ///// <param name="jql">QJL statement of the filter.</param>
    ///// <param name="description">Description of the filter.</param>
    ///// <param name="isFavourite">Set favourite flag of the filter.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    ///// <remarks>Only for JIRA 5.2.1 or later.</remarks>
    //public static async Task<Filter?> CreateFilterAsync(this Jira jira, string name, string jql, string description, bool isFavourite = true)
    //{
    //    return await jira.CreateFilterAsync(new Filter() { Name = name, Jql = jql, Description = description, IsFavourite = isFavourite });
    //}

    ///// <summary>
    ///// Adds a new comment to an issue.
    ///// </summary>
    ///// <param name="jira">The Jira class.</param>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="body">Body of the comment.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public static async Task<Comment?> AddCommentAsync(this Jira jira, string issueIdOrKey, string body)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNullOrEmpty(body, nameof(body));
       
    //    return await jira.AddCommentAsync(issueIdOrKey, new Comment() { Body = body });
    //}

    ///// <summary>
    ///// Create an isue remote link.
    ///// </summary>
    ///// <param name="jira">The Jira class.</param>
    ///// <param name="issueIdOrKey">Key of the issue to link from.</param>
    ///// <param name="serverName">Name of the serve to link to.</param>
    ///// <param name="link">Url to link to.</param>
    ///// <param name="relationship">Name of the link relationship.</param>
    ///// <param name="title">Title of the link</param>
    ///// <param name="summary">Summary of the linked issue</param>
    ///// <param name="icon">Icon of the linked issue.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public static async Task<RemoteLink?> CreateIssueRemoteLinkAsync(this Jira jira, string issueIdOrKey, string serverName, Uri link, string relationship, string? title = null, string? summary = null, Icon? icon = null)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNullOrEmpty(serverName, nameof(serverName));
    //    ArgumentNullException.ThrowIfNull(link, nameof(link));
    //    ArgumentNullException.ThrowIfNullOrEmpty(relationship, nameof(relationship));

    //    var remoteLink = new RemoteLink();
    //    remoteLink.GlobalId = Guid.NewGuid().ToString(); 
    //    remoteLink.Application = new Application() { Type = "com.atlassian.jira", Name = serverName };
    //    remoteLink.Relationship = relationship;
    //    remoteLink.Object = new JiraWebApi.Object() { Url = link, Title = title, Summary = summary, Icon = icon };
    //    return await jira.CreateIssueRemoteLinkAsync(issueIdOrKey, remoteLink);
    //}

    ///// <summary>
    ///// Add one attachment to an issue.
    ///// </summary>
    ///// <param name="jira">The Jira class.</param>
    ///// <param name="issueIdOrKey">Id or key of the issue.</param>
    ///// <param name="fileName">File name of the attachment.</param>
    ///// <param name="stream">Stream of the attachment data.</param>
    ///// <returns>The task object representing the asynchronous operation.</returns>
    //public static async Task<Attachment?> AddAttachmentAsync(this Jira jira, string issueIdOrKey, string fileName, Stream stream)
    //{
    //    ArgumentNullException.ThrowIfNullOrEmpty(issueIdOrKey, nameof(issueIdOrKey));
    //    ArgumentNullException.ThrowIfNullOrEmpty(fileName, nameof(fileName));
    //    ArgumentNullException.ThrowIfNull(stream, nameof(stream));
        
    //    IEnumerable<Attachment>? attachments = await jira.AddAttachmentsAsync(issueIdOrKey, new KeyValuePair<string, Stream>[] { new(fileName, stream) });
    //    return attachments?.FirstOrDefault();
    //}
}
