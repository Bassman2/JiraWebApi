using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class VersionMoveAfterPostRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("after")]
        public string After { get; set; }

    }
}
