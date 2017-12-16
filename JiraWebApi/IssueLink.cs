using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue link.
    /// </summary>
    public sealed class IssueLink
    {
        /// <summary>
        /// Initializes a new instance of the Link class.
        /// </summary>
        public IssueLink()
        { }

        /// <summary>
        /// Id of the issue link.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; private set; }

        /// <summary>
        /// Type of the issue link.
        /// </summary>
        [JsonProperty("type")]
        public IssueLinkType Type { get; set; }

        /// <summary>
        /// Inward issue of the issue link.
        /// </summary>
        [JsonProperty("inwardIssue")]
        public Issue InwardIssue { get; set; }

        /// <summary>
        /// Outward issue of the issue link.
        /// </summary>
        [JsonProperty("outwardIssue")]
        public Issue OutwardIssue { get; set; }

        /// <summary>
        /// Add a comment during issue link creation.
        /// </summary>
        /// <remarks>
        /// Writeonly: Not for getting the comment.
        /// </remarks>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public Comment Comment { private get; set; }
    }
}
