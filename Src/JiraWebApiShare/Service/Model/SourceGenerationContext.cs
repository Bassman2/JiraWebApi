namespace JiraWebApi.Service.Model;

[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(ServerInfoModel))]
[JsonSerializable(typeof(IssueTypeModel))]
[JsonSerializable(typeof(IEnumerable<IssueTypeModel>))]
[JsonSerializable(typeof(ProjectModel))]
[JsonSerializable(typeof(IEnumerable<ProjectModel>))]
[JsonSerializable(typeof(IssueModel))]
[JsonSerializable(typeof(CreateMetaModel))]

internal partial class SourceGenerationContext : JsonSerializerContext
{ }
