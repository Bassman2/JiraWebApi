using Newtonsoft.Json;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue link type.
    /// </summary>
    public class IssueLinkTypeBase
    {
        /// <summary>
        /// Initializes a new instance of the IssueLinkTypeBase class.
        /// </summary>
        public IssueLinkTypeBase()
        { }

        /// <summary>
        /// Name of the JIRA item.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the inward link.
        /// </summary>
        [JsonProperty("inward")]
        public string Inward { get; set; }

        /// <summary>
        /// Name od the outward link.
        /// </summary>
        [JsonProperty("outward")]
        public string Outward { get; set; }
    }
}
