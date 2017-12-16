using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue worklog. 
    /// </summary>
    public sealed class Worklog : Element
    {
        /// <summary>
        /// Initializes a new instance of the Worklog class.
        /// </summary>
        public Worklog()
        { }

        /// <summary>
        /// Author of the JIRA issue worklog.
        /// </summary>
        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public User Author { get; set; }

        /// <summary>
        /// Update author of the JIRA issue worklog.
        /// </summary>
        [JsonProperty("updateAuthor", NullValueHandling = NullValueHandling.Ignore)]
        public User UpdateAuthor { get; set; }

        /// <summary>
        /// Comment of the JIRA issue worklog.
        /// </summary>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        /// <summary>
        /// Visibility of the JIRA issue worklog.
        /// </summary>
        [JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
        public Visibility Visibility { get; set; }

        /// <summary>
        /// Start date of the JIRA issue worklog.
        /// </summary>
        [JsonProperty("started", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Started { get; set; }

        /// <summary>
        /// Time spend of the JIRA issue worklog.
        /// </summary>
        [JsonProperty("timeSpent", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeSpent { get; set; }

        /// <summary>
        /// Time spend in seconds of the JIRA issue worklog.
        /// </summary>
        [JsonProperty("timeSpentSeconds", /*NullValueHandling = NullValueHandling.Ignore,*/ DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int TimeSpentSeconds { get; set; }
    }
}
