namespace JiraWebApi;

/// <summary>
/// Meta infromatins for the fields.
/// </summary>
[DebuggerDisplay("{Name} {IsRequired}")]
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
    public Schema? Schema { get; private set; }

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
