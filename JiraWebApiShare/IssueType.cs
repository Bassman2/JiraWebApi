using System;
using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue type. 
    /// </summary>
    public sealed class IssueType : ComparableElement
    {
        /// <summary>
        /// Initializes a new instance of the IssueType class.
        /// </summary>
        internal IssueType()
        { }

        /// <summary>
        /// Description of the JIRA issue type.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Url of the issue type icon.
        /// </summary>
        [JsonPropertyName("iconUrl")]
        public Uri IconUrl { get; private set; }

        /// <summary>
        /// Signals if the issue type is a subtask issue type.
        /// </summary>
        [JsonPropertyName("subtask")]
        public bool IsSubtask { get; private set; }

        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        [JsonPropertyName("expand")]
        public string Expand { get; private set; }

        /// <summary>
        /// Fields which are editable for this issue type. Used by meta information.
        /// </summary>
        /// <remarks>Only filled by <see cref="Jira.GetCreateMetaAsync">GetCreateMetaAsync</see> and <see cref="Jira.GetEditMetaAsync">GetEditMetaAsync</see>.</remarks>
        [JsonPropertyName("fields")]
        public Fields Fields { get; private set; }
                
        /// <summary>
        /// Returns a string that represents the issue type.
        /// </summary>
        /// <returns>A string that represents the issue type.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
