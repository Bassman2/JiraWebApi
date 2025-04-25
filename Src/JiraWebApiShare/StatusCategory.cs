namespace JiraWebApi;

/// <summary>
/// Represents a JIRA status category, which groups statuses into logical categories such as "To Do," "In Progress," or "Done."
/// </summary>
public class StatusCategory
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StatusCategory"/> class using the provided <see cref="StatusCategoryModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="StatusCategoryModel"/> containing the data to initialize the status category.</param>
    internal StatusCategory(StatusCategoryModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Key = model.Key;
        ColorName = model.ColorName;
        Name = model.Name;
    }

    /// <summary>
    /// Gets the URL of the JIRA REST resource for this status category.
    /// </summary>
    public Uri? Self { get; }

    /// <summary>
    /// Gets the unique identifier of the status category.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the key of the status category, which is a unique string identifier.
    /// </summary>
    public string? Key { get; }

    /// <summary>
    /// Gets the name of the color associated with the status category.
    /// </summary>
    /// <remarks>
    /// The color name is used to visually distinguish the status category in the JIRA interface.
    /// </remarks>
    public string? ColorName { get; }

    /// <summary>
    /// Gets the name of the status category.
    /// </summary>
    /// <remarks>
    /// Common examples include "To Do," "In Progress," and "Done."
    /// </remarks>
    public string? Name { get; }
}
