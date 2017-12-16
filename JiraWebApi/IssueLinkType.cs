using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue link type.
    /// </summary>
    public sealed class IssueLinkType : ComparableElement
    {
        /// <summary>
        /// Initializes a new instance of the IssueLinkType class.
        /// </summary>
        private IssueLinkType()
        { }

        /// <summary>
        /// Name of the inward link.
        /// </summary>
        [JsonProperty("inward")]
        public string Inward { get; private set; }

        /// <summary>
        /// Name od the outward link.
        /// </summary>
        [JsonProperty("outward")]
        public string Outward { get; private set; }
    }
}
