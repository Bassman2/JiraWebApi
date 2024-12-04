namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA component. 
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class Component : ComparableElementModel
{
    /// <summary>
    /// Initializes a new instance of the Component class.
    /// </summary>
    public Component()
    { }

    /// <summary>
    /// Static method to represent the JQL componentsLeadByUser() function
    /// </summary>
    /// <returns>No result.</returns>
    /// <remarks>For Linq use only.</remarks>
    [JqlFunction("componentsLeadByUser")]
    public static Component ComponentsLeadByUser()
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Name of the JIRA project description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; } 

    /// <summary>
    /// Project to which the version belongs.
    /// </summary>
    [JsonPropertyName("project")]
    public string? ProjectKey { private get; set; }
}
