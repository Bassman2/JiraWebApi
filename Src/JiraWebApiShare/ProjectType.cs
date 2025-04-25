namespace JiraWebApi;

/// <summary>
/// Represents a JIRA project type, which defines the characteristics and behavior of a project.
/// </summary>
public class ProjectType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectType"/> class using the provided <see cref="ProjectTypeModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="ProjectTypeModel"/> containing the data to initialize the project type.</param>
    internal ProjectType(ProjectTypeModel model)
    {
        Key = model.Key;
        FormattedKey = model.FormattedKey;
        DescriptionI18nKey = model.DescriptionI18nKey;
        Icon = model.Icon;
        Color = model.Color;
    }

    /// <summary>
    /// Gets the unique key of the project type.
    /// </summary>
    /// <remarks>
    /// The key is a unique identifier for the project type, such as "software" or "business."
    /// </remarks>
    public string? Key { get; }

    /// <summary>
    /// Gets the formatted key of the project type.
    /// </summary>
    /// <remarks>
    /// The formatted key is a user-friendly version of the key, often used for display purposes.
    /// </remarks>
    public string? FormattedKey { get; }

    /// <summary>
    /// Gets the internationalization key for the description of the project type.
    /// </summary>
    /// <remarks>
    /// This key is used to retrieve localized descriptions of the project type.
    /// </remarks>
    public string? DescriptionI18nKey { get; }

    /// <summary>
    /// Gets the URL or identifier of the icon associated with the project type.
    /// </summary>
    /// <remarks>
    /// The icon visually represents the project type in the JIRA interface.
    /// </remarks>
    public string? Icon { get; }

    /// <summary>
    /// Gets the color associated with the project type.
    /// </summary>
    /// <remarks>
    /// The color is often used to visually distinguish the project type in the JIRA interface.
    /// </remarks>
    public string? Color { get; }
}
