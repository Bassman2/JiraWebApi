namespace JiraWebApi;

/// <summary>
/// Representation of meta information for issue creation.
/// </summary>
public sealed class CreateMeta
{
    /// <summary>
    /// Initializes a new instance of the CreateMeta class.
    /// </summary>
    internal CreateMeta()
    { }

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    public string? Expand { get; init; }

    /// <summary>
    /// Projects for which tickets can be created by the user.
    /// </summary>
    public IEnumerable<Project>? Projects { get; init; }
}
