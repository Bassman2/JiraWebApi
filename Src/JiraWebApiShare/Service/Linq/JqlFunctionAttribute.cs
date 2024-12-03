namespace JiraWebApi.Service.Linq;

[AttributeUsage(AttributeTargets.Method)]
internal class JqlFunctionAttribute : Attribute
{
    public string Name { get; set; }

    /// <summary>
    /// Initializes a new instance of the JqlFunctionAttribute class.
    /// </summary>
    public JqlFunctionAttribute(string name)
    {
        this.Name = name;
    }
}
