using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class ComponentRelatedIssueCounts
    {
        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("issueCount")]
        public int IssueCount { get; set; }
    }
}
