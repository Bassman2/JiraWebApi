using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of JIRA roles. 
    /// </summary>
    public sealed class Roles
    {
        /// <summary>
        /// Initializes a new instance of the Roles class.
        /// </summary>
        private Roles()
        { }

        /// <summary>
        /// E-mail of the JIRY project.
        /// </summary>
        [JsonProperty("Developers")]
        public Uri Developers { get; private set; }
    }
}
