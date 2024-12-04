namespace JiraWebApi.Service.Model;

internal class SessionGetResult
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("loginInfo")]
    public LoginInfo? LoginInfo { get; set; }
}
