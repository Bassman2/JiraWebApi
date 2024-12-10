namespace JiraWebApi;

/// <summary>
/// Positions to move versions.
/// </summary>
public enum Position
{
    /// <summary>
    /// Move version to first position.
    /// </summary>
    First,

    /// <summary>
    /// Move version to last position
    /// </summary>
    Last,

    /// <summary>
    /// Move version to an earlier position.
    /// </summary>
    Earlier,

    /// <summary>
    /// Move version to a later version.
    /// </summary>
    Later
}
