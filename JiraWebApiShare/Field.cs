using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Rrepresentation of a JIRA field. 
    /// </summary>
    public sealed class Field
    {
        /// <summary>
        /// Initializes a new instance of the Field class.
        /// </summary>
        private Field()
        { }

        /// <summary>
        /// Id of the JIRA field.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the JIRA field.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Signals if the JIRA field is a custom field.
        /// </summary>
        [JsonPropertyName("custom")]
        public bool IsCustom { get; set; }

        /// <summary>
        /// Signals if the JIRA field is orderable.
        /// </summary>
        [JsonPropertyName("orderable")]
        public bool IsOrderable { get; set; }

        /// <summary>
        /// Signals if the JIRA field is navigable.
        /// </summary>
        [JsonPropertyName("navigable")]
        public bool IsNavigable { get; set; }

        /// <summary>
        /// Signals if the JIRA field is searchable.
        /// </summary>
        [JsonPropertyName("searchable")]
        public bool IsSearchable { get; set; }

        /// <summary>
        /// Schema of the JIRA field.
        /// </summary>
        [JsonPropertyName("schema")]
        public Schema Schema { get; set; }

        /// <summary>
        /// Returns a string that represents the issue field.
        /// </summary>
        /// <returns>A string that represents the issue field.</returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}{3}{4}{5}, {6}", this.Id, this.Name, this.IsCustom ? "C" : "", this.IsOrderable ? "O" : "", this.IsNavigable ? "N" : "", this.IsSearchable ? "S" : "", this.Schema);
        }
    }
}
