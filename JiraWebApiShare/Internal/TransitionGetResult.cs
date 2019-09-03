using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class TransitionGetResult
    {
        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        [JsonPropertyName("expand")]
        public string Expand { get; set; }

        [JsonPropertyName("transitions")]
        public IEnumerable<Transition> Transitions { get; set; }
    }
}
