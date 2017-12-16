using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue type. 
    /// </summary>
    public sealed class IssueType : ComparableElement
    {
        /// <summary>
        /// Initializes a new instance of the IssueType class.
        /// </summary>
        internal IssueType()
        { }

        /// <summary>
        /// Description of the JIRA issue type.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Url of the issue type icon.
        /// </summary>
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; private set; }

        /// <summary>
        /// Signals if the issue type is a subtask issue type.
        /// </summary>
        [JsonProperty("subtask")]
        public bool IsSubtask { get; private set; }

        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        [JsonProperty("expand")]
        public string Expand { get; private set; }

        /// <summary>
        /// Fields which are editable for this issue type. Used by meta information.
        /// </summary>
        /// <remarks>Only filled by <see cref="Jira.GetCreateMetaAsync">GetCreateMetaAsync</see> and <see cref="Jira.GetEditMetaAsync">GetEditMetaAsync</see>.</remarks>
        [JsonProperty("fields")]
        public Fields Fields { get; private set; }
                
        /// <summary>
        /// Returns a string that represents the issue type.
        /// </summary>
        /// <returns>A string that represents the issue type.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
