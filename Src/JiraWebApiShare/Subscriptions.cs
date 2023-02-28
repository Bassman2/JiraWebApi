using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Users who has subscribed to an issue.
    /// </summary>
    public sealed class Subscriptions
    {
        /// <summary>
        /// Initializes a new instance of the Subscriptions class.
        /// </summary>
        private Subscriptions()
        { }

        /// <summary>
        /// Number of the subscriptions.
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; private set; }

        /// <summary>
        /// Users who has subscribed.
        /// </summary>
        [JsonPropertyName("items")]
        public IEnumerable<User> Items { get; private set; }
    }
}
