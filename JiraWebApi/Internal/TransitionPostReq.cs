using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class TransitionPostReq
    {
        [JsonProperty("update")]
        public object Update { get; set; }

        [JsonProperty("fields")]
        public Fields Fields { get; set; }

        [JsonProperty("transition")]
        public Transition Transition { get; set; }
    }
}
