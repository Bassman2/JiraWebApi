using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Object of a remote link.
    /// </summary>
    public sealed class Object
    {
        /// <summary>
        /// Initializes a new instance of the Object class.
        /// </summary>
        public Object()
        { }

        /// <summary>
        /// Url of the remote link object.
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// Title of the remote link object.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Summary of the remote link object.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Icon of the remote link object.
        /// </summary>
        [JsonProperty("icon")]
        public Icon Icon { get; set; }

        // TODO
        //[JsonProperty("status")]
        //public Status Status { get; set; }
    }
}
