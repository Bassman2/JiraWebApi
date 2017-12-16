using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class WorklogGetResult
    {
        [JsonProperty(PropertyName = "startAt")]
        public int StartAt { get; set; }

        [JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "worklogs")]
        public IEnumerable<Worklog> Worklogs { get; set; }
    }
}
