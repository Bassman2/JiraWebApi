namespace JiraWebApi.Service.Model;

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true, AllowTrailingCommas = true, NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(ServerInfoModel))]
[JsonSerializable(typeof(IssueTypeModel))]
[JsonSerializable(typeof(IEnumerable<IssueTypeModel>))]
[JsonSerializable(typeof(ProjectModel))]
[JsonSerializable(typeof(IEnumerable<ProjectModel>))]
[JsonSerializable(typeof(IEnumerable<ComponentModel>))]
[JsonSerializable(typeof(IssueModel))]
[JsonSerializable(typeof(CreateMetaModel))]
[JsonSerializable(typeof(CreateIssueModel))]

[JsonSerializable(typeof(ErrorModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{ }
