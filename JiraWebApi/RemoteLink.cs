using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Rrepresentation of a JIRA issue remote link. 
    /// </summary>
    public sealed class RemoteLink : Element
    {
        /// <summary>
        /// Initializes a new instance of the RemoteLink class.
        /// </summary>
        public RemoteLink()
        { }

        /// <summary>
        /// Global Id of the JIRA issue global link.
        /// </summary>
        [JsonProperty("globalId", NullValueHandling = NullValueHandling.Ignore)]
        public string GlobalId { get; set; }
        
        /// <summary>
        /// Application of the remote link.
        /// </summary>
        [JsonProperty("application")]
        public Application Application { get; set; }

        /// <summary>
        /// Relationship of the JIRA issue global link.
        /// </summary>
        [JsonProperty("relationship")]
        public string Relationship { get; set; }

        /// <summary>
        /// Object of the remote link.
        /// </summary>
        [JsonProperty("object")]
        public Object Object { get; set; }
    }
}
