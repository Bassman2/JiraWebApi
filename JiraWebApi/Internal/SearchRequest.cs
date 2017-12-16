using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class SearchRequest
    {
        [JsonProperty("jql")]
        public string Jql { private get; set; }

        [JsonProperty("startAt")]
        public int StartAt { private get; set; }

        [JsonProperty("maxResults")]
        public int MaxResults { private get; set; }

        [JsonProperty("fields")]
        public string Fields { private get; set; }

        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        [JsonProperty("expand")]
        public string Expand { private get; set; }

    }
}
