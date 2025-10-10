namespace JiraWebApi.Model;

internal class VersionMoveAfterPostRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("after")]
    public string? After { get; set; }

}
