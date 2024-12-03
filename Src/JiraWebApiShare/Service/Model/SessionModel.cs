namespace JiraWebApi.Service.Model;

internal class SessionModel
{

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    public static implicit operator Session?(SessionModel? model)
    {
        return model is null ? null : new Session()
        {
            Name = model.Name,
            Value = model.Value
        };
    }
}
