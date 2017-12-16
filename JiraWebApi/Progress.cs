using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Progress of an issue.
    /// </summary>
    public sealed class Progress
    {
        /// <summary>
        /// Initializes a new instance of the Progress class.
        /// </summary>
        private Progress()
        { }

        /// <summary>
        /// Progress values.
        /// </summary>
        [JsonProperty("progress")]
        public int Value { get; private set; }
        
        /// <summary>
        /// Total progress value.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; private set; }

        /// <summary>
        /// Progress percentage.
        /// </summary>
        [JsonProperty("percent")]
        public int Percent { get; private set; }
    }
}
