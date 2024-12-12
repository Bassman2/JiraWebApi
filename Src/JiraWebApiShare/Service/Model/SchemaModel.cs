namespace JiraWebApi.Service.Model;


/// <summary>
/// Rrepresentation of a JIRA schema. 
/// </summary>
[DebuggerDisplay("Schema: {Type}, {Items}, {System}, {Custom}, {CustomId}")]
internal class SchemaModel
{
    

    /// <summary>
    /// Type of the JIRA schema.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Items of the JIRA schema.
    /// </summary>
    [JsonPropertyName("items")]
    public string? Items { get; set; }

    /// <summary>
    /// System of the JIRA schema.
    /// </summary>
    [JsonPropertyName("system")]
    public string? System { get; set; }

    /// <summary>
    /// Value type of the custom field.
    /// </summary>
    [JsonPropertyName("custom")]
    public string? Custom { get; set; }

    /// <summary>
    /// Custom field type Id.
    /// </summary>
    [JsonPropertyName("customId")]
    public int CustomId { get; set; }
}
