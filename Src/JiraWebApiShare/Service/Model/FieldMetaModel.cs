namespace JiraWebApi.Service.Model;

/// <summary>
/// Meta infromatins for the fields.
/// </summary>
[DebuggerDisplay("{Name} {IsRequired}")]
internal class FieldMetaModel
{
    /// <summary>
    /// Is field required.
    /// </summary>
    [JsonPropertyName("required")]
    public bool IsRequired { get; set; }

    /// <summary>
    /// Schema of the field.
    /// </summary>
    [JsonPropertyName("schema")]
    public SchemaModel? Schema { get; set; }

    /// <summary>
    /// Name of the field.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Auto complete URL.
    /// </summary>
    [JsonPropertyName("autoCompleteUrl")]
    public string? AutoCompleteUrl { get; set; }

    /// <summary>
    /// Operations.
    /// </summary>
    [JsonPropertyName("operations")]
    public List<string>? Operations { get; set; }
}
