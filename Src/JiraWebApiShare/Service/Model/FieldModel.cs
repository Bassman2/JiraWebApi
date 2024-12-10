namespace JiraWebApi.Service.Model;


/// <summary>
/// Rrepresentation of a JIRA field. 
/// </summary>
[DebuggerDisplay("{Id}, {Name}, {this.IsCustom}{this.IsOrderable}{IsNavigable}{IsSearchable}, {Schema}")]
internal sealed class FieldModel
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
    public string? Id { get; set; }

    /// <summary>
    /// Name of the JIRA field.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Signals if the JIRA field is a custom field.
    /// </summary>
    [JsonPropertyName("custom")]
    public bool IsCustom { get; set; }

    /// <summary>
    /// Signals if the JIRA field is orderable.
    /// </summary>
    [JsonPropertyName("orderable")]
    public bool? IsOrderable { get; set; }

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
    public Schema? Schema { get; set; }
}
