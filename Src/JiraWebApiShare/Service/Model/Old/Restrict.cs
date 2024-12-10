namespace JiraWebApi;

/// <summary>
/// Representation of a restrict.
/// </summary>
public sealed class Restrict
{
    /// <summary>
    /// Initializes a new instance of the Restrict class.
    /// </summary>
    private Restrict()
    { }

    /// <summary>
    /// Groups of the restriction.
    /// </summary>
    [JsonPropertyName("groups")]
    public IEnumerable<Group>? Groups { get; set; }

    /// <summary>
    /// Permissions of the restriction.
    /// </summary>
    [JsonPropertyName("permissions")]
    public IEnumerable<Permission>? Permission { get; set; }
}
