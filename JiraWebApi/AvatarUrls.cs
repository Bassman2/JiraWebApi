using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [JsonProperty("16x16")]
        public Uri Small { get; private set; }

        /// <summary>
        /// Url of the large avatar with 48x48 pixel.
        /// </summary>
        [JsonProperty("48x48")]
        public Uri Large { get; private set; }
    }
}
