namespace JiraWebApi.Service.Model;

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true, AllowTrailingCommas = true)]
[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(ServerInfoModel))]
[JsonSerializable(typeof(IssueTypeModel))]
[JsonSerializable(typeof(IEnumerable<IssueTypeModel>))]
[JsonSerializable(typeof(ProjectModel))]
[JsonSerializable(typeof(IEnumerable<ProjectModel>))]
[JsonSerializable(typeof(IssueModel))]
[JsonSerializable(typeof(CreateMetaModel))]
[JsonSerializable(typeof(CreateIssueModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{ }
