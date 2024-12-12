namespace JiraWebApi;

public class Status
{
    internal Status(StatusModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Name = model.Name;
        Description = model.Description;
        IconUrl = model.IconUrl;
        StatusCategory = model.StatusCategory.CastModel<StatusCategory>();
    }

    public Uri? Self { get; }

    public int Id { get; }

    public string? Name { get; }

    public string? Description { get; }

    public Uri? IconUrl { get; }

    public StatusCategory? StatusCategory { get; set; }
}
