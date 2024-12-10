namespace JiraWebApi;

/// <summary>
/// Type of custom field
/// </summary>
public enum CustomFieldType
{
    /// <summary>
    /// Empty custom field.
    /// </summary>
    Empty,

    /// <summary>
    /// Custom field with string item.
    /// </summary>
    String,

    /// <summary>
    /// Custom field with DateTime item.
    /// </summary>
    DateTime,

    /// <summary>
    /// Custom field with double item.
    /// </summary>
    Double,
    
    /// <summary>
    /// Custom field with Project item.
    /// </summary>
    Project,

    /// <summary>
    /// Custom field with Version item.
    /// </summary>
    Version,

    /// <summary>
    /// Custom field with Group item.
    /// </summary>
    Group,

    /// <summary>
    /// Custom field with User item.
    /// </summary>
    User,

    /// <summary>
    /// Custom field with string array item.
    /// </summary>
    StringArray,

    /// <summary>
    /// Custom field with Group array item.
    /// </summary>
    GroupArray,

    /// <summary>
    /// Custom field with Version array item.
    /// </summary>
    VersionArray,

    /// <summary>
    /// Custom field with User array item.
    /// </summary>
    UserArray      
}
