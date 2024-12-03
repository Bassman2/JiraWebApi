namespace JiraWebApi.Service.Linq;

[AttributeUsage(AttributeTargets.Property)]
internal class JqlFieldAttribute : Attribute
{
    public string Name { get; set; }

    public JqlFieldCompare Compare { get; set; }

    /// <summary>
    /// Initializes a new instance of the JqlFieldAttribute class.
    /// </summary>
    public JqlFieldAttribute(string name, JqlFieldCompare compare)
    {
        this.Name = name;
        this.Compare = compare;
    }
}
