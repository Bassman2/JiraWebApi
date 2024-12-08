
namespace JiraWebApi.Service.Model;


[DebuggerDisplay("{Id} {Name}")]
internal class CustomField
{
    public CustomField(Field field, CustomFieldValue value)
    {
        this.Id = field.Id;
        this.Name = field.Name;
        this.Value = value;
    }

    public string? Id { get; set; }
    public string? Name { get; set; }
    public CustomFieldValue? Value { get; set; }
    public bool Changed { get; set; }
}
