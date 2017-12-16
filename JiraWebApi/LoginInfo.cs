using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA login info. 
    /// </summary>
    public sealed class LoginInfo
    {
        /// <summary>
        /// Initializes a new instance of the LoginInfo class.
        /// </summary>
        private LoginInfo()
        { }

        /// <summary>
        /// Number of failed logins of the current user.
        /// </summary>
        [JsonProperty("failedLoginCount")]
        public int FailedLoginCount { get; set; }
        
        /// <summary>
        /// Number of logins of the current user.
        /// </summary>
        [JsonProperty("loginCount")]
        public int LoginCount { get; set; }

        /// <summary>
        /// Date of the last failed login of the current user.
        /// </summary>
        [JsonProperty("lastFailedLoginTime")]
        public DateTime LastFailedLoginTime { get; set; }

        /// <summary>
        /// Date of the previous login of the current user.
        /// </summary>
        [JsonProperty("previousLoginTime")]
        public DateTime PreviousLoginTime { get; set; }
    }
}
