namespace JiraWebApi.Service.Model;

internal class CommentGetResult
{
    [JsonPropertyName("startAt")]
    public int StartAt { get; set; }

    [JsonPropertyName("maxResults")]
    public int MaxResults { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("comments")]
    public IEnumerable<Comment>? Comments { get; set; }
}
