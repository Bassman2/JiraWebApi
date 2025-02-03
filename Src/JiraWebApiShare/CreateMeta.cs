namespace JiraWebApi;

/// <summary>
/// Representation of meta information for issue creation.
/// </summary>
public  class CreateMeta
{
    /// <summary>
    /// Initializes a new instance of the CreateMeta class.
    /// </summary>
    internal CreateMeta(CreateMetaModel model)
    {
        Expand = model.Expand;
        Projects = model.Projects.CastModel<Project>();
    }

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    public string? Expand { get; }

    /// <summary>
    /// Projects for which tickets can be created by the user.
    /// </summary>
    public List<Project>? Projects { get; }
}
