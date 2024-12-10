namespace JiraWebApi.Service.Model;

internal class VersionMovePositionPostRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Only First, Last, Earlier or Later allowed.</remarks>
    [JsonPropertyName("position")]
    public Position Position { get; set; }
}
