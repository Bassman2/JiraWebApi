namespace JiraWebApi.Service.Model;

internal class AssignPutRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
