namespace JiraWebApi.Service.Model;

internal class SessionModel
{
    [JsonPropertyName("self")]
    public string? Self { get; set; }


    [JsonPropertyName("name")]
    public string? Name { get; set; }

    //[JsonPropertyName("value")]
    //public string? Value { get; set; }

    public static implicit operator Session?(SessionModel? model)
    {
        return model is null ? null : new Session()
        {
            Self = model.Self,
            Name = model.Name,
            //Value = model.Value
        };
    }
}
