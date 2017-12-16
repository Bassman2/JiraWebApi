using JiraWebApi.Internal;
using JiraWebApi.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA component. 
    /// </summary>
    public sealed class Component : ComparableElement
    {
        /// <summary>
        /// Initializes a new instance of the Component class.
        /// </summary>
        public Component()
        { }

        /// <summary>
        /// Static method to represent the JQL componentsLeadByUser() function
        /// </summary>
        /// <returns>No result.</returns>
        /// <remarks>For Linq use only.</remarks>
        [JqlFunction("componentsLeadByUser")]
        public static Component ComponentsLeadByUser()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Name of the JIRA project description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } 

        /// <summary>
        /// Project to which the version belongs.
        /// </summary>
        [JsonProperty("project")]
        public string ProjectKey { private get; set; }

        /// <summary>
        /// Returns a string that represents the component.
        /// </summary>
        /// <returns>A string that represents the component.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
