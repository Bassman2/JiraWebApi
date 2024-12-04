namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue resolution. 
/// </summary>
[DebuggerDisplay("{Id}, {Name}, {Description}")]
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
    public string? Description { get; private set; }
}
