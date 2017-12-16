using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue attachment. 
    /// </summary>
    public sealed class Attachment : Element
    {
        /// <summary>
        /// Initializes a new instance of the Attachment class.
        /// </summary>
        public Attachment()
        { }

        /// <summary>
        /// File name of the JIRA attachment.
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; private set; }

        /// <summary>
        /// Author of the JIRA attachment.
        /// </summary>
        [JsonProperty("author")]
        public User Author { get; private set; }

        /// <summary>
        /// Creation data of the JIRA attachment.
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; private set; }

        /// <summary>
        /// Size of the JIRA attachment.
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; private set; }

        /// <summary>
        /// Mime type of the JIRA attachment.
        /// </summary>
        [JsonProperty("mimeType")]
        public string MimeType { get; private set; }

        /// <summary>
        /// Url of the content of the JIRA attachment.
        /// </summary>
        [JsonProperty("content")]
        public Uri Content { get; private set; }

        /// <summary>
        /// Url of the thumbnail of the JIRA attachment.
        /// </summary>
        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; private set; }
    }
}
