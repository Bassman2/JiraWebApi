namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of meta information for issue creation.
/// </summary>
internal class CreateMetaModel
{
   

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; private set; }

    /// <summary>
    /// Projects for which tickets can be created by the user.
    /// </summary>
    [JsonPropertyName("projects")]
    public IEnumerable<Project>? Projects { get; private set; }

    public static implicit operator CreateMeta?(CreateMetaModel? model)
    {
        return model is null ? null : new CreateMeta()
        {
            //BaseUrl = model.BaseUrl!,
            //Version = new Version(model.Version ?? "0.0.0"),
            //DeploymentType = model.DeploymentType ?? "",
            //BuildDate = model.BuildDate ?? default,
            //ServerTime = model.ServerTime ?? default,
            //ScmInfo = model.ScmInfo ?? "",
            //ServerTitle = model.ServerTitle!,
        };
    }
}
