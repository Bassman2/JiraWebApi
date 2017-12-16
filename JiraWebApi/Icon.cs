using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Represents a JIRA icon of a remoteLink object.
    /// </summary>
    public sealed class Icon
    {
        /// <summary>
        /// Initializes a new instance of the Icon class.
        /// </summary>
        public Icon()
        { }

        /// <summary>
        /// Url of the JIRA icon.
        /// </summary>
        [JsonProperty("url16x16")]
        public Uri Url16x16 { get; private set; }

        /// <summary>
        /// Title of the JIRA icon.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Url of the JIRA icon.
        /// </summary>
        [JsonProperty("link")]
        public Uri Link { get; private set; }
    }
}
