using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of attachment meta information. 
    /// </summary>
    public sealed class AttachmentMeta
    {
        /// <summary>
        /// Initializes a new instance of the AttachmentMeta class.
        /// </summary>
        private AttachmentMeta()
        { }

        /// <summary>
        /// Signals if attachments enabled on this JIRA server.
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// Maximum allowed attachment size.
        /// </summary>
        [JsonProperty("uploadLimit")]
        public long UploadLimit { get; private set; }
    }
}
