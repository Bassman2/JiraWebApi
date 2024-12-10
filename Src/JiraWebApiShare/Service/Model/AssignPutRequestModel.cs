namespace JiraWebApi.Service.Model;

internal class AssignPutRequestModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
