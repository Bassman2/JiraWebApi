using System;
using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Urls to the JIRA avatar icons.
    /// </summary>
    public sealed class AvatarUrls
    {
        /// <summary>
        /// Initializes a new instance of the AvatarUrls class.
        /// </summary>
        private AvatarUrls()
        { }

        /// <summary>
        /// Url of the small avatar with 16x16 pixel.
        /// </summary>
        [JsonPropertyName("16x16")]
        public Uri Small { get; private set; }

        /// <summary>
        /// Url of the large avatar with 48x48 pixel.
        /// </summary>
        [JsonPropertyName("48x48")]
        public Uri Large { get; private set; }
    }
}
