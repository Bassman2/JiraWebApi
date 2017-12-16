using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class TransitionGetResult
    {
        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        [JsonProperty("expand")]
        public string Expand { get; set; }

        [JsonProperty("transitions")]
        public IEnumerable<Transition> Transitions { get; set; }
    }
}
