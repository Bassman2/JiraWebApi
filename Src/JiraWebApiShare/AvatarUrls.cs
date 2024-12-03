namespace JiraWebApi;

/// <summary>
/// Urls to the JIRA avatar icons.
/// </summary>
public class AvatarUrls
{
    /// <summary>
    /// Url of the small avatar with 16x16 pixel.
    /// </summary>
    public Uri? AvatarUrl16 { get; internal init; }

    /// <summary>
    /// Url of the small avatar with 24x24 pixel.
    /// </summary>
    public Uri? AvatarUrl24 { get; internal init; }

    /// <summary>
    /// Url of the large avatar with 32x32 pixel.
    /// </summary>
    public Uri? AvatarUrl32 { get; internal init; }

    /// <summary>
    /// Url of the large avatar with 48x48 pixel.
    /// </summary>
    public Uri? AvatarUrl48 { get; internal init; }
}
