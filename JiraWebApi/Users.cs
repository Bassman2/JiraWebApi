using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// List of users.
    /// </summary>
    public sealed class Users
    {
        internal Users()
        { }

        /// <summary>
        /// Number of users
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; private set; }

        /// <summary>
        /// List of users.
        /// </summary>
        [JsonProperty("items")]
        public IEnumerable<User> Items { get; private set; }

        /// <summary>
        /// Max number of users to get.
        /// </summary>
        [JsonProperty("max-results")]
        public int MaxResults { get; private set; }

        /// <summary>
        /// Start index of user get window.
        /// </summary>
        [JsonProperty("start-index")]
        public int StartIndex { get; private set; }

        /// <summary>
        /// End index of user get window.
        /// </summary>
        [JsonProperty("end-index")]
        public int EndIndex { get; private set; }
    }

}
