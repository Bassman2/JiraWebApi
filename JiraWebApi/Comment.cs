using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue comment. 
    /// </summary>
    public sealed class Comment : Element
    {
        /// <summary>
        /// Initializes a new instance of the Comment class.
        /// </summary>
        public Comment()
        { }

        /// <summary>
        /// Author of the JIRA comment.
        /// </summary>
        [JsonProperty("author")]
        public User Author { get; private set; }

        /// <summary>
        /// Body of the JIRA comment.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Update author of the JIRA comment.
        /// </summary>
        [JsonProperty("updateAuthor")]
        public User UpdateAuthor { get; private set; }

        /// <summary>
        /// Creation data of the JIRA comment.
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; internal set; }

        /// <summary>
        /// Update data of the JIRA comment.
        /// </summary>
        [JsonProperty("updated")]
        public DateTime? Updated { get; internal set; }

        /// <summary>
        /// Visibility of the JIRA comment.
        /// </summary>
        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }
    }
}
