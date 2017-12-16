using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class VersionUnresolvedIssueCount
    {
        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("issuesUnresolvedCount")]
        public int IssuesUnresolvedCount { get; set; }
    }
}
