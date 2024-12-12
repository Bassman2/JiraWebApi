namespace JiraWebApi.Service.Model;

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true, AllowTrailingCommas = true, NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(IEnumerable<ComponentModel>))]
[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(IssueTypeModel))]
[JsonSerializable(typeof(IEnumerable<IssueTypeModel>))]
[JsonSerializable(typeof(IEnumerable<PriorityModel>))]
[JsonSerializable(typeof(PriorityPageModel))]
[JsonSerializable(typeof(ProjectModel))]
[JsonSerializable(typeof(IEnumerable<ProjectModel>))]
[JsonSerializable(typeof(IEnumerable<ProjectTypeModel>))]
[JsonSerializable(typeof(IEnumerable<ResolutionModel>))]

[JsonSerializable(typeof(IssueModel))]
[JsonSerializable(typeof(CreateMetaModel))]
[JsonSerializable(typeof(CreateIssueModel))]
[JsonSerializable(typeof(ServerInfoModel))]
[JsonSerializable(typeof(IEnumerable<StatusModel>))]
[JsonSerializable(typeof(IEnumerable<StatusCategoryModel>))]

[JsonSerializable(typeof(ErrorModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{ }
