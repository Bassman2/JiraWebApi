using Newtonsoft.Json;

namespace JiraWebApi
{
    public class CustomFieldBase
    {
        /// <summary>
        /// Initializes a new instance of the CustomFieldBase class.
        /// </summary>
        public CustomFieldBase()
        { }

        /// <summary>
        /// Id of the JIRA item.
        /// </summary>
        [JsonProperty("id")]
        [JsonIgnore()]
        public string Id { get; set; }

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
        /// Type of the CustomField.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Type of the CustomField.
        /// </summary>
        [JsonProperty("searcherKey")]
        public string SearcherKey { get; set; }
    }
}
