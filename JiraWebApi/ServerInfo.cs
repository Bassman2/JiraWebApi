using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Informations about the JIRA server.
    /// </summary>
    public sealed class ServerInfo
    {
        /// <summary>
        /// Initializes a new instance of the ServerInfo class.
        /// </summary>
        private ServerInfo()
        { }

        /// <summary>
        /// Base web address of the JIRA server.
        /// </summary>
        [JsonProperty(PropertyName = "baseUrl")]
        public Uri BaseUrl { get; private set; }

        /// <summary>
        /// Build version of the JIRA server release version.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; private set; }

        /// <summary>
        /// Version number of the JIRA server.
        /// </summary>
        [JsonProperty(PropertyName = "versionNumbers")]
        private int[] VersionNumbers 
        {   
            set
            {
                if (value.Length >= 3)
                {
                    this.MajorVersionNumber = value[0];
                    this.MinorVersionNumber = value[1];
                    this.RevisionVersionNumber = value[2];
                }
            }
        }

        /// <summary>
        /// Major version of the JIRA server.
        /// </summary>
        [JsonIgnore]
        public int MajorVersionNumber { get; private set; }

        /// <summary>
        /// Minor version of the JIRA server.
        /// </summary>
        [JsonIgnore]
        public int MinorVersionNumber { get; private set; }

        /// <summary>
        /// Revision number of the JIRA server.
        /// </summary>
        [JsonIgnore]
        public int RevisionVersionNumber { get; private set; }

        /// <summary>
        /// Build number of the JIRA server release version.
        /// </summary>
        [JsonProperty(PropertyName = "buildNumber")]
        public int BuildNumber { get; private set; }

        /// <summary>
        /// Build date of the JIRA server release version.
        /// </summary>
        [JsonProperty(PropertyName = "buildDate")]
        public DateTime? BuildDate { get; private set; }

        /// <summary>
        /// Current server time.
        /// </summary>
        [JsonProperty(PropertyName = "serverTime")]
        public DateTime? ServerTime { get; private set; }

        /// <summary>
        /// Time zone of the server location.
        /// </summary>
        [JsonProperty(PropertyName = "scmInfo")]
        public string ScmInfo { get; private set; }

        /// <summary>
        /// Name of the Atlassian build partner.
        /// </summary>
        [JsonProperty(PropertyName = "buildPartnerName")]
        public string BuildPartnerName { get; private set; }

        /// <summary>
        /// Title of the JIRA server.
        /// </summary>
        [JsonProperty(PropertyName = "serverTitle")]
        public string ServerTitle { get; private set; }
    }
}
