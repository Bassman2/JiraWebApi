namespace JiraWebApi;

public class StatusCategory
{
    internal StatusCategory(StatusCategoryModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Key = model.Key;
        ColorName = model.ColorName;
        Name = model.Name;
    }
    public Uri? Self { get; }

    public int Id { get; }

    public string? Key { get; }

    public string? ColorName { get; }

    public string? Name { get; }
}
