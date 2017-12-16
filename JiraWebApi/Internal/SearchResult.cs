using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class SearchResult
    {
        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        [JsonProperty("expand")]
        public string Expand { get; private set; }
        
        [JsonProperty("startAt")]
        public int StartAt { get; private set; }

        [JsonProperty("maxResults")]
        public int MaxResults { get; private set; }

        [JsonProperty("total")]
        public int Total { get; private set; }

        [JsonProperty("issues")]
        public IEnumerable<Issue> Issues { get; private set; }
    }
}
