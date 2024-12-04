namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue priority. 
/// </summary>
[DebuggerDisplay("{Id}, {Name}, {Description}")]
public sealed class Priority : SortableElement
{
    /// <summary>
    /// Initializes a new instance of the Priority class.
    /// </summary>
    internal Priority()
    { }

    /// <summary>
    /// Description of the JIRA priority.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; private set; }

    /// <summary>
    /// Url of the icon of the JIRA priority.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri? IconUrl { get; private set; }

    /// <summary>
    /// Status color of the JIRA priority.
    /// </summary>
    [JsonPropertyName("statusColor")]
    public string? StatusColor { get; private set; }
}
