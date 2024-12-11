namespace JiraWebApi.Service.Model;

internal class ProjectTypeModel
{
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("formattedKey")]
    public string? FormattedKey { get; set; }

    [JsonPropertyName("descriptionI18nKey")]
    public string? DescriptionI18nKey { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }
}
