namespace JiraWebApi.Service.Model;

internal class VersionMoveAfterPostRequest
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("after")]
    public string? After { get; set; }

}
