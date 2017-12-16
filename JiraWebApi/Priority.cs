using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue priority. 
    /// </summary>
    public sealed class Priority : SortableElement
    {
        /// <summary>
        /// Initializes a new instance of the Priority class.
        /// </summary>
        internal Priority()
        { }

        /// <summary>
        /// Description of the JIRA priority.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Url of the icon of the JIRA priority.
        /// </summary>
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; private set; }

        /// <summary>
        /// Status color of the JIRA priority.
        /// </summary>
        [JsonProperty("statusColor")]
        public string StatusColor { get; private set; }

        ///// <summary>
        ///// Returns a string that represents the issue priority.
        ///// </summary>
        ///// <returns>A string that represents the issue priority.</returns>
        //public override string ToString()
        //{
        //    return string.Format("{0}, {1}, {2}", this.Id, this.Name, this.Description);
        //}
    }
}
