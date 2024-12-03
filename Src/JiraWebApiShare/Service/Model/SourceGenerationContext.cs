namespace JiraWebApi.Service.Model;


[JsonSerializable(typeof(SessionPostRequestModel))]  
[JsonSerializable(typeof(SessionPostResultModel))]
[JsonSerializable(typeof(IssueTypeModel))]
[JsonSerializable(typeof(IEnumerable<IssueTypeModel>))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
