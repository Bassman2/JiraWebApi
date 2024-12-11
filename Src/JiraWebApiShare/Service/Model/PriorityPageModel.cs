namespace JiraWebApi.Service.Model;

internal class PriorityPageModel
{

    [JsonPropertyName("maxResults")]
    public int MaxResults { get; set; }

    [JsonPropertyName("startAt")]
    public int startAt { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("isLast")]
    public bool IsLast { get; set; }

    
    [JsonPropertyName("values")]
    public IEnumerable<PriorityModel>? Values { get; set; }
}

// {"maxResults":100,"startAt":0,"total":0,"isLast":true,"values":[]}