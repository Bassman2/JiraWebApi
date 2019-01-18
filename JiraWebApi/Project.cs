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
    /// Rrepresentation of a JIRA project. 
    /// </summary>
    public sealed class Project : ComparableElement
    {
        /// <summary>
        /// Initializes a new instance of the Project class.
        /// </summary>
        internal Project()
        { }

        /// <summary>
        /// Support of the JQL 'projectsLeadByUser()' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        /// <remarks>For Linq use only.</remarks>
        public static Project[] ProjectsLeadByUser()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'projectsWhereUserHasPermission()' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        /// <remarks>For Linq use only.</remarks>
        [JqlFunction("projectsWhereUserHasPermission")]
        public static Project[] ProjectsWhereUserHasPermission()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'projectsWhereUserHasRole()' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        /// <remarks>For Linq use only.</remarks>
        [JqlFunction("projectsWhereUserHasRole")]
        public static Project[] ProjectsWhereUserHasRole()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }
        
        /// <summary>
        /// Key of the JIRY project.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; private set; }
                
        /// <summary>
        /// Description of the JIRY project.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Icon URL of the JIRY project.
        /// </summary>
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; private set; }

        /// <summary>
        /// Lead of the JIRY project.
        /// </summary>
        [JsonProperty("lead")]
        public User Lead { get; private set; }

        /// <summary>
        /// Components of the JIRY project.
        /// </summary>
        [JsonProperty("components")]
        public IEnumerable<Component> Components { get; private set; }

        /// <summary>
        /// Issue types of the JIRY project.
        /// </summary>
        [JsonProperty("issueTypes")]
        public IEnumerable<IssueType> IssueTypes { get; set; }

        /// <summary>
        /// URL of the JIRY project.
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; private set; }

        /// <summary>
        /// E-mail of the JIRY project.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; private set; }

        /// <summary>
        /// Assignee type of the JIRY project.
        /// </summary>
        [JsonProperty("assigneeType")]
        public string AssigneeType { get; private set; }

        /// <summary>
        /// Versions of the JIRY project.
        /// </summary>
        [JsonProperty("versions")]
        public IEnumerable<IssueVersion> Versions { get; private set; }

        /// <summary>
        /// Roles of the JIRY project.
        /// </summary>
        [JsonProperty("roles")]
        public Roles roles { get; private set; }

        /// <summary>
        /// Avatar URLs of the JIRY project.
        /// </summary>
        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; private set; }
    }
}
