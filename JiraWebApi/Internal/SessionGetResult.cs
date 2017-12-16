using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class SessionGetResult
    {
        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("loginInfo")]
        public LoginInfo LoginInfo { get; set; }
    }
}
