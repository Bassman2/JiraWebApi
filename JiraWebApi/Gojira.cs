﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for easier JIRA access. 
    /// </summary>
    public static class Gojira
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
        public static async Task<Issue> CreateIssueAsync(this Jira jira, string type, string project, string summary, string description = null)
        {
            IEnumerable<IssueType> issueTypes = await jira.GetIssueTypesAsync();
            IEnumerable<Project> projects = await jira.GetProjectsAsync();
            Issue issue = new Issue();
            issue.IssueType = issueTypes.Where(t => t.Name == type).First();
            issue.Project = projects.Where(p => p.Name == project).First();
            issue.Summary = summary;
            issue.Description = description;
            return await jira.CreateIssueAsync(issue);
        }

        /// <summary>
        /// Create an issue link.
        /// </summary>
        /// <param name="jira">The Jira class.</param>
        /// <param name="issueLinkTypeName">Name of the link type.</param>
        /// <param name="inwardIssueKey">Key of the inward issue.</param>
        /// <param name="outwardIssueKey">Key of the outward issue.</param>
        /// <param name="comment">Comment to add.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task CreateIssueLinkAsync(this Jira jira, string issueLinkTypeName, string inwardIssueKey, string outwardIssueKey, string comment = null)
        {
            IEnumerable<IssueLinkType> issueLinkTypes = await jira.GetIssueLinkTypesAsync();
            IssueLinkType issueLinkType = issueLinkTypes.Where(t => t.Name == issueLinkTypeName).FirstOrDefault();

            //IssueLink link = new IssueLink();
            //link.Type = issueLinkTypes.Where(t => t.Name == issueLinkTypeName).FirstOrDefault();
            //link.Comment = string.IsNullOrEmpty(comment) ? null : new Comment() { Body = "comment" };
            //link.InwardIssue = new Issue(inwardIssueKey);
            //link.OutwardIssue = new Issue(outwardIssueKey);
            //await jira.CreateIssueLinkAsync(link);

            await jira.CreateIssueLinkAsync(issueLinkType, inwardIssueKey, outwardIssueKey, comment);
        }

        /// <summary>
        /// Create an issue link.
        /// </summary>
        /// <param name="jira">The Jira class.</param>
        /// <param name="issueLinkType">Issue link type.</param>
        /// <param name="inwardIssueKey">Issue key of the inward issue.</param>
        /// <param name="outwardIssueKey">Issue key of the outward issue.</param>
        /// <param name="comment">A comment created with the link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task CreateIssueLinkAsync(this Jira jira, IssueLinkType issueLinkType, string inwardIssueKey, string outwardIssueKey, string comment = null)
        {
            //IssueLink link = new IssueLink();
            //link.Type = issueLinkType;
            //link.Comment = string.IsNullOrEmpty(comment) ? null : new Comment() { Body = "comment" };
            //link.InwardIssue = new Issue(inwardIssueKey);
            //link.OutwardIssue = new Issue(outwardIssueKey);
            //await jira.CreateIssueLinkAsync(link);
            await jira.CreateIssueLinkAsync(issueLinkType, new Issue(inwardIssueKey), new Issue(outwardIssueKey), comment);
        }

        /// <summary>
        /// Create an issue link
        /// </summary>
        /// <param name="jira">The Jira class.</param>
        /// <param name="issueLinkType">Issue link type.</param>
        /// <param name="inwardIssue">The inward issue.</param>
        /// <param name="outwardIssue">Teh outward issue.</param>
        /// <param name="comment">A comment created with the link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task CreateIssueLinkAsync(this Jira jira, IssueLinkType issueLinkType, Issue inwardIssue, Issue outwardIssue, string comment = null)
        {
            IssueLink link = new IssueLink();
            link.Type = issueLinkType;
            link.Comment = string.IsNullOrEmpty(comment) ? null : new Comment() { Body = "comment" };
            link.InwardIssue = inwardIssue;
            link.OutwardIssue = outwardIssue;
            await jira.CreateIssueLinkAsync(link);
        }

        /// <summary>
        /// Creates a new filter, and returns newly created filter Currently sets permissions just using the users default sharing permissions.
        /// </summary>
        /// <param name="jira">The Jira class.</param>
        /// <param name="name">Name of the filter to create.</param>
        /// <param name="jql">QJL statement of the filter.</param>
        /// <param name="description">Description of the filter.</param>
        /// <param name="isFavourite">Set favourite flag of the filter.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        public static async Task<Filter> CreateFilterAsync(this Jira jira, string name, string jql, string description, bool isFavourite = true)
        {
            return await jira.CreateFilterAsync(new Filter() { Name = name, Jql = jql, Description = description, IsFavourite = isFavourite });
        }

        /// <summary>
        /// Adds a new comment to an issue.
        /// </summary>
        /// <param name="jira">The Jira class.</param>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="body">Body of the comment.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<Comment> AddCommentAsync(this Jira jira, string issueIdOrKey, string body)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentException("The body is null or empty.");
            }

            return await jira.AddCommentAsync(issueIdOrKey, new Comment() { Body = body });
        }

        /// <summary>
        /// Create an isue remote link.
        /// </summary>
        /// <param name="jira">The Jira class.</param>
        /// <param name="issueIdOrKey">Key of the issue to link from.</param>
        /// <param name="serverName">Name of the serve to link to.</param>
        /// <param name="link">Url to link to.</param>
        /// <param name="relationship">Name of the link relationship.</param>
        /// <param name="title">Title of the link</param>
        /// <param name="summary">Summary of the linked issue</param>
        /// <param name="icon">Icon of the linked issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<RemoteLink> CreateIssueRemoteLinkAsync(this Jira jira, string issueIdOrKey, string serverName, Uri link, string relationship, string title = null, string summary = null, Icon icon = null)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(serverName))
            {
                throw new ArgumentException("The serverName is null or empty.");
            }
            if (link == null)
            {
                throw new ArgumentNullException("link");
            } 
            if (string.IsNullOrEmpty(relationship))
            {
                throw new ArgumentException("The relationship is null or empty.");
            }
            
            RemoteLink remoteLink = new RemoteLink();
            remoteLink.GlobalId = Guid.NewGuid().ToString(); 
            remoteLink.Application = new Application() { Type = "com.atlassian.jira", Name = serverName };
            remoteLink.Relationship = relationship;
            remoteLink.Object = new JiraWebApi.Object() { Url = link, Title = title, Summary = summary, Icon = icon };
            return await jira.CreateIssueRemoteLinkAsync(issueIdOrKey, remoteLink);
        }

        /// <summary>
        /// Add one attachment to an issue.
        /// </summary>
        /// <param name="jira">The Jira class.</param>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="fileName">File name of the attachment.</param>
        /// <param name="stream">Stream of the attachment data.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static async Task<Attachment> AddAttachmentAsync(this Jira jira, string issueIdOrKey, string fileName, Stream stream)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("The fileName is null or empty.");
            }
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            IEnumerable<Attachment> attachments = await jira.AddAttachmentsAsync(issueIdOrKey, new KeyValuePair<string, Stream>[] { new KeyValuePair<string, Stream>(fileName, stream) });
            return attachments.FirstOrDefault();
        }
    }
}
