using JiraWebApi.Internal;
using JiraWebApi.Linq;
using System;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("description")]
        public string Description { get; set; } 

        /// <summary>
        /// Project to which the version belongs.
        /// </summary>
        [JsonPropertyName("project")]
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
