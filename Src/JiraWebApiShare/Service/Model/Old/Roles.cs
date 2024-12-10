namespace JiraWebApi;

/// <summary>
/// Representation of JIRA roles. 
/// </summary>
public sealed class Roles
{
    /// <summary>
    /// Initializes a new instance of the Roles class.
    /// </summary>
    public Roles()
    { }

    /// <summary>
    /// E-mail of the JIRY project.
    /// </summary>
    [JsonPropertyName("Developers")]
    public Uri? Developers { get; private set; }
}
