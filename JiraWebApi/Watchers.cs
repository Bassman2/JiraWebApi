using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of JIRA issue watchers.
    /// </summary>
    public sealed class Watchers
    {
        /// <summary>
        /// Initializes a new instance of the Watchers class.
        /// </summary>
        private Watchers()
        { }

        /// <summary>
        /// Url of the REST item.
        /// </summary>
        [JsonProperty("self")]
        public Uri Self { get; private set; }

        /// <summary>
        /// Number of registered watchers.
        /// </summary>
        [JsonProperty("watchCount")]
        public int WatchCount { get; private set; }

        /// <summary>
        /// Signals if someone watches the issue.
        /// </summary>
        [JsonProperty("isWatching")]
        public bool IsWatching { get; private set; }

        /// <summary>
        /// List of registered watchers.
        /// </summary>
        /// <remarks>Only filled by GetIssueWatchersAsync.</remarks>
        [JsonProperty("watchers")]
        public IEnumerable<User> Users { get; private set; }
    }
}
