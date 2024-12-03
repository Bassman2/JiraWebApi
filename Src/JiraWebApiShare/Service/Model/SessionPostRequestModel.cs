namespace JiraWebApi.Service.Model;

internal class SessionPostRequestModel
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
