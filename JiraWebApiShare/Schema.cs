using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Rrepresentation of a JIRA schema. 
    /// </summary>
    public sealed class Schema
    {
        /// <summary>
        /// Initializes a new instance of the Schema class.
        /// </summary>
        private Schema()
        { }

        /// <summary>
        /// Type of the JIRA schema.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Items of the JIRA schema.
        /// </summary>
        [JsonPropertyName("items")]
        public string Items { get; set; }

        /// <summary>
        /// System of the JIRA schema.
        /// </summary>
        [JsonPropertyName("system")]
        public string System { get; set; }

        /// <summary>
        /// Value type of the custom field.
        /// </summary>
        [JsonPropertyName("custom")]
        public string Custom { get; set; }

        /// <summary>
        /// Custom field type Id.
        /// </summary>
        [JsonPropertyName("customId")]
        public int CustomId { get; set; }

        /// <summary>
        /// Returns a string that represents the Schema.
        /// </summary>
        /// <returns>A string element.</returns>
        public override string ToString()
        {
            return string.Format("Schema: {0}, {1}, {2}, {3}, {4}", this.Type, this.Items, this.System, this.Custom, this.CustomId);
        }
    }
}
