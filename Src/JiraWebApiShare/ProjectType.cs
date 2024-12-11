namespace JiraWebApi;

public class ProjectType
{
    internal ProjectType(ProjectTypeModel model)
    { 
        Key = model.Key;
        FormattedKey = model.FormattedKey;
        DescriptionI18nKey = model.DescriptionI18nKey;
        Icon = model.Icon;
        Color = model.Color;
    }

    public string? Key { get; }

    public string? FormattedKey { get; }

    public string? DescriptionI18nKey { get; }

    public string? Icon { get; }

    public string? Color { get; }
}
