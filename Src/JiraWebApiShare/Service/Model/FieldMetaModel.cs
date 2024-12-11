namespace JiraWebApi.Service.Model;

/// <summary>
/// Meta infromatins for the fields.
/// </summary>
[DebuggerDisplay("{Name} {IsRequired}")]
internal sealed class FieldMetaModel
{
    /// <summary>
    /// Is field required.
    /// </summary>
    [JsonPropertyName("required")]
    public bool IsRequired { get; private set; }

    /// <summary>
    /// Schema of the field.
    /// </summary>
    [JsonPropertyName("schema")]
    public SchemaModel? Schema { get; private set; }

    /// <summary>
    /// Name of the field.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; private set; }

    /// <summary>
    /// Auto complete URL.
    /// </summary>
    [JsonPropertyName("autoCompleteUrl")]
    public string? AutoCompleteUrl { get; private set; }

    /// <summary>
    /// Operations.
    /// </summary>
    [JsonPropertyName("operations")]
    public IEnumerable<string>? Operations { get; private set; }
}
