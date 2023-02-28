using System;
using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Represents a JIRA icon of a remoteLink object.
    /// </summary>
    public sealed class Icon
    {
        /// <summary>
        /// Initializes a new instance of the Icon class.
        /// </summary>
        public Icon()
        { }

        /// <summary>
        /// Url of the JIRA icon.
        /// </summary>
        [JsonPropertyName("url16x16")]
        public Uri Url16x16 { get; private set; }

        /// <summary>
        /// Title of the JIRA icon.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Url of the JIRA icon.
        /// </summary>
        [JsonPropertyName("link")]
        public Uri Link { get; private set; }
    }
}
