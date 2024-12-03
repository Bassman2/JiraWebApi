namespace JiraWebApi.Service.Model;

internal class IssueLinkTypesRespnse
{
    [JsonPropertyName("issueLinkTypes")]
    public IEnumerable<IssueLinkType> IssueLinkTypes { get; set; }
}
