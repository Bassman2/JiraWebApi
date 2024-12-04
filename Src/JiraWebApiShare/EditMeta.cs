namespace JiraWebApi;

/// <summary>
/// Representation of meta information for issue editing.
/// </summary>
public sealed class EditMeta
{
    /// <summary>
    /// Initializes a new instance of the EditMeta class.
    /// </summary>
    private EditMeta()
    { }

    /// <summary>
    /// Available fields for issue editing.
    /// </summary>
    [JsonPropertyName("fields")]
    public Fields? Fields { get; private set; }
}
