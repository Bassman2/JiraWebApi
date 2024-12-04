namespace JiraWebApi.Service.Model;


[JsonSerializable(typeof(SessionPostRequestModel))]  
[JsonSerializable(typeof(SessionPostResultModel))]
[JsonSerializable(typeof(IssueTypeModel))]
[JsonSerializable(typeof(IEnumerable<IssueTypeModel>))]
[JsonSerializable(typeof(ProjectModel))]
[JsonSerializable(typeof(IEnumerable<ProjectModel>))]
[JsonSerializable(typeof(IssueModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
