using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Represent an Application for remote links. 
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// Initializes a new instance of the Application class.
        /// </summary>
        public Application()
        { }

        /// <summary>
        /// Type of the application.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
