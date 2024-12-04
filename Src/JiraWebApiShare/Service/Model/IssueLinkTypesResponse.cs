namespace JiraWebApi.Service.Model;

internal class IssueLinkTypesRespnse
{
    [JsonPropertyName("issueLinkTypes")]
    public List<IssueLinkType>? IssueLinkTypes { get; set; }
}
