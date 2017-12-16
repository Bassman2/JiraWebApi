using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class VersionMovePositionPostRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Only First, Last, Earlier or Later allowed.</remarks>
        [JsonProperty("position")]
        public Position Position { get; set; }
    }
}
