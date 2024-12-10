namespace JiraWebApi.Service.Model;

internal class TransitionGetResult
{
    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; set; }

    [JsonPropertyName("transitions")]
    public IEnumerable<TransitionModel>? Transitions { get; set; }
}
