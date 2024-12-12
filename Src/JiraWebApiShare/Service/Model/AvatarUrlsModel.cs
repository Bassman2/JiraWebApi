namespace JiraWebApi.Service.Model;

/// <summary>
/// Urls to the JIRA avatar icons.
/// </summary>
public class AvatarUrlsModel
{
    /// <summary>
    /// Url of the small avatar with 16x16 pixel.
    /// </summary>
    [JsonPropertyName("16x16")]
    public Uri? AvatarUrl16 { get; set; }

    /// <summary>
    /// Url of the large avatar with 48x48 pixel.
    /// </summary>
    [JsonPropertyName("24x24")]
    public Uri? AvatarUrl24 { get; set; }

    /// <summary>
    /// Url of the large avatar with 48x48 pixel.
    /// </summary>
    [JsonPropertyName("32x32")]
    public Uri? AvatarUrl32 { get; set; }

    /// <summary>
    /// Url of the large avatar with 48x48 pixel.
    /// </summary>
    [JsonPropertyName("48x48")]
    public Uri? AvatarUrl48 { get; set; }
}
