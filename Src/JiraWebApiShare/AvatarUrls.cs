namespace JiraWebApi;

/// <summary>
/// Urls to the JIRA avatar icons.
/// </summary>
public class AvatarUrls
{
    internal AvatarUrls(AvatarUrlsModel model)
    {
        AvatarUrl16 = model.AvatarUrl16;
        AvatarUrl24 = model.AvatarUrl24;
        AvatarUrl32 = model.AvatarUrl32;
        AvatarUrl48 = model.AvatarUrl48;
    }

    /// <summary>
    /// Url of the small avatar with 16x16 pixel.
    /// </summary>
    public Uri? AvatarUrl16 { get; }

    /// <summary>
    /// Url of the small avatar with 24x24 pixel.
    /// </summary>
    public Uri? AvatarUrl24 { get; }

    /// <summary>
    /// Url of the large avatar with 32x32 pixel.
    /// </summary>
    public Uri? AvatarUrl32 { get; }

    /// <summary>
    /// Url of the large avatar with 48x48 pixel.
    /// </summary>
    public Uri? AvatarUrl48 { get; }
}
