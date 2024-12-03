namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue link type.
    /// </summary>
    public sealed class IssueLinkType : ComparableElementModel
    {
        /// <summary>
        /// Initializes a new instance of the IssueLinkType class.
        /// </summary>
        private IssueLinkType()
        { }

        /// <summary>
        /// Name of the inward link.
        /// </summary>
        [JsonPropertyName("inward")]
        public string Inward { get; private set; }

        /// <summary>
        /// Name od the outward link.
        /// </summary>
        [JsonPropertyName("outward")]
        public string Outward { get; private set; }
    }
}
