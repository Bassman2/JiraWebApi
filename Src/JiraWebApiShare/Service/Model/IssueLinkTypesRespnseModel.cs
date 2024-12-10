namespace JiraWebApi.Service.Model;

internal class IssueLinkTypesRespnseModel
{
    [JsonPropertyName("issueLinkTypes")]
    public List<IssueLinkTypeModel>? IssueLinkTypes { get; set; }
}
