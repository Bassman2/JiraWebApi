namespace JiraWebApi.Service.Model;

/// <summary>
/// Urls to the JIRA avatar icons.
/// </summary>
public sealed class AvatarUrlsModel
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

    public static implicit operator AvatarUrls?(AvatarUrlsModel? model)
    {
        return model is null ? null : new AvatarUrls()
        {
            AvatarUrl16 = model.AvatarUrl16,
            AvatarUrl24 = model.AvatarUrl24,
            AvatarUrl32 = model.AvatarUrl32,
            AvatarUrl48 = model.AvatarUrl48
        };
    }
}
