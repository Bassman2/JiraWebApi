using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class SessionPostResult
    {
        internal class Session_
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "value")]
            public string Value { get; set; }

        }

        [JsonProperty(PropertyName = "session")]
        public Session_ Session { get; set; }

        [JsonProperty(PropertyName = "loginInfo")]
        public LoginInfo LoginInfo { get; set; }

    }
}
