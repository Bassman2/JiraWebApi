using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA group.
    /// </summary>
    public sealed class Group
    {
        /// <summary>
        /// Initializes a new instance of the Group class.
        /// </summary>
        public Group()
        { }

        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonProperty("self")]
        public string Self { get; set; }

        /// <summary>
        /// Name of the JIRA group.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Users of the JIRA group.
        /// </summary>
        [JsonProperty("users")]
        public Users Users { get; set; }

        /// <summary>
        /// Names of the expanded fields.
        /// </summary>
        [JsonProperty("expand")]
        public string Expand { get; set; }
    }
}
