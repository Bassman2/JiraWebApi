namespace JiraWebApi.Service.Model;

internal class CommentListModel
{
    [JsonPropertyName("startAt")]
    public int StartAt { get; set; }

    [JsonPropertyName("maxResults")]
    public int MaxResults { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("comments")]
    public IEnumerable<CommentModel>? Comments { get; set; }
}
