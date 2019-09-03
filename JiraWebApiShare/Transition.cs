using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue transition.
    /// </summary>
    public sealed class Transition : Element
    {
        /// <summary>
        /// Initializes a new instance of the Transition class.
        /// </summary>
        public Transition()
        { }

        /// <summary>
        /// Name of the issue transition.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Result status of this issue transition.
        /// </summary>
        [JsonPropertyName("to")]
        public Status To { get; set; }

        /// <summary>
        /// Fields which are editable during this transition.
        /// </summary>
        [JsonPropertyName("fields")]
        public Fields Fields { get; set; }
    }
}
