using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Meta infromatins for the fields.
    /// </summary>
    public sealed class FieldMeta
    {
        /// <summary>
        /// Initializes a new instance of the FieldMeta class.
        /// </summary>
        private FieldMeta()
        { }

        /// <summary>
        /// Is field required.
        /// </summary>
        [JsonPropertyName("required")]
        public bool IsRequired { get; private set; }

        /// <summary>
        /// Schema of the field.
        /// </summary>
        [JsonPropertyName("schema")]
        public Schema Schema { get; private set; }

        /// <summary>
        /// Name of the field.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Auto complete URL.
        /// </summary>
        [JsonPropertyName("autoCompleteUrl")]
        public string AutoCompleteUrl { get; private set; }

        /// <summary>
        /// Operations.
        /// </summary>
        [JsonPropertyName("operations")]
        public IEnumerable<string> Operations { get; private set; }

        ///// <summary>
        ///// Allowed values.
        ///// </summary>
        //[JsonPropertyName("allowedValues")]
        //public IEnumerable<JObject> AllowedValues { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.Name, this.IsRequired ? "R" : "");
        }
    }
}
