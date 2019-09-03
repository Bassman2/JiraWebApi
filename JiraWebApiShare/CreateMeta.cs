using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of meta information for issue creation.
    /// </summary>
    public sealed class CreateMeta
    {
        /// <summary>
        /// Initializes a new instance of the CreateMeta class.
        /// </summary>
        private CreateMeta()
        { }

        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        [JsonPropertyName("expand")]
        public string Expand { get; private set; }

        /// <summary>
        /// Projects for which tickets can be created by the user.
        /// </summary>
        [JsonPropertyName("projects")]
        public IEnumerable<Project> Projects { get; private set; }
    }
}
