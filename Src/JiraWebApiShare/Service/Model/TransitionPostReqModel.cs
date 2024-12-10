namespace JiraWebApi.Service.Model;

internal class TransitionPostReqModel
{
    [JsonPropertyName("update")]
    public object? Update { get; set; }

    //[JsonPropertyName("fields")]
    //public Fields? Fields { get; set; }

    [JsonPropertyName("transition")]
    public TransitionModel? Transition { get; set; }
}
