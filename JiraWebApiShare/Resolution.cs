using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue resolution. 
    /// </summary>
    public sealed class Resolution : SortableElement
    {
        /// <summary>
        /// Initializes a new instance of the Resolution class.
        /// </summary>
        internal Resolution()
        { }

        /// <summary>
        /// Description of the JIRA resolution.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; private set; }

        ///// <summary>
        ///// Returns a string that represents the issue resolution.
        ///// </summary>
        ///// <returns>A string that represents the issue resolution.</returns>
        //public override string ToString()
        //{
        //    return string.Format("{0}, {1}, {2}", this.Id, this.Name, this.Description);
        //}
    }
}
