using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Rrepresentation of the visibility of a JIRA item. 
    /// </summary>
    public sealed class Visibility
    {
        /// <summary>
        /// Initializes a new instance of the Visibility class.
        /// </summary>
        public Visibility()
        { }

        /// <summary>
        /// Type of the JIRA visibility.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Value of the JIRA visibility.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
