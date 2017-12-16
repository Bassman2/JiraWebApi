using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class VersionRelatedIssueCounts
    {
        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("issuesFixedCount")]
        public int IssuesFixedCount { get; set; }

        [JsonProperty("issuesAffectedCount")]
        public int IssuesAffectedCount { get; set; }
    }
}
