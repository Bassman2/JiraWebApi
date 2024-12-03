using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Rrepresentation of a JIRA issue remote link. 
    /// </summary>
    public sealed class RemoteLink : ElementModel
    {
        /// <summary>
        /// Initializes a new instance of the RemoteLink class.
        /// </summary>
        public RemoteLink()
        { }

        /// <summary>
        /// Global Id of the JIRA issue global link.
        /// </summary>
        [JsonPropertyName("globalId")]
        public string GlobalId { get; set; }
        
        /// <summary>
        /// Application of the remote link.
        /// </summary>
        [JsonPropertyName("application")]
        public Application Application { get; set; }

        /// <summary>
        /// Relationship of the JIRA issue global link.
        /// </summary>
        [JsonPropertyName("relationship")]
        public string Relationship { get; set; }

        /// <summary>
        /// Object of the remote link.
        /// </summary>
        [JsonPropertyName("object")]
        public Object Object { get; set; }
    }
}
