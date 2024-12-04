namespace JiraWebApi.Service.Model;

internal class TransitionPostReq
{
    [JsonPropertyName("update")]
    public object? Update { get; set; }

    [JsonPropertyName("fields")]
    public Fields? Fields { get; set; }

    [JsonPropertyName("transition")]
    public Transition? Transition { get; set; }
}
