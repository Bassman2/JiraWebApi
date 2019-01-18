using Newtonsoft.Json;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue type.
    /// </summary>
    public class IssueTypeBase
    {
        /// <summary>
        /// Initializes a new instance of the IssueTypeBase class.
        /// </summary>
        public IssueTypeBase()
        { }

        /// <summary>
        /// Name of the JIRA item.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the JIRA issue type.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Type of the IssueType.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
