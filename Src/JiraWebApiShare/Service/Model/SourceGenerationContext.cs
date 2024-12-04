namespace JiraWebApi.Service.Model;

[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(ServerInfoModel))]
[JsonSerializable(typeof(IssueTypeModel))]
[JsonSerializable(typeof(IEnumerable<IssueTypeModel>))]
[JsonSerializable(typeof(ProjectModel))]
[JsonSerializable(typeof(IEnumerable<ProjectModel>))]
[JsonSerializable(typeof(IssueModel))]


internal partial class SourceGenerationContext : JsonSerializerContext
{ }
