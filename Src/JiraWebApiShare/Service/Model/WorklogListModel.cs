namespace JiraWebApi.Service.Model;

internal class WorklogListModel
{
    [JsonPropertyName("startAt")]
    public int StartAt { get; set; }

    [JsonPropertyName("maxResults")]
    public int MaxResults { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("worklogs")]
    public IEnumerable<WorklogModel>? Worklogs { get; set; }
}
