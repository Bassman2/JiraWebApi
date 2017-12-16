using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representatiojn of a JIRA notification e-mail.
    /// </summary>
    public sealed class Notify
    {
        /// <summary>
        /// Initializes a new instance of the Notify class.
        /// </summary>
        public Notify()
        { }

        /// <summary>
        /// Subject of the notification e-mail.
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Text body of the notification e-mail.
        /// </summary>
        [JsonProperty("textBody")]
        public string TextBody { get; set; }

        /// <summary>
        /// HTML body of the notification e-mail.
        /// </summary>
        [JsonProperty("htmlBody")]
        public string HtmlBody { get; set; }

        /// <summary>
        /// Addresses of the notification e-mail.
        /// </summary>
        [JsonProperty("to")]
        public NotifyTo To { get; set; }

        /// <summary>
        /// Restriction of the notification.
        /// </summary>
        [JsonProperty("restrict", NullValueHandling = NullValueHandling.Ignore)]
        public Restrict Restrict { get; set; }
    }
}
