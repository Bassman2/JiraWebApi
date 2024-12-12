namespace JiraWebApiUnitTest;

[TestClass()]
public class JiraComponentUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetComponentsAsync()
    {
        using var jira = new Jira(storeKey);

        var project = await jira.GetProjectByKeyAsync(testProjectKey);
        Assert.IsNotNull(project);

        var res = await jira.GetComponentsAsync(project);
        Assert.IsNotNull(res);

        var components = res?.ToList();
        Assert.IsNotNull(components);
        
        var component = components.FirstOrDefault();
        Assert.IsNotNull(component);

        Assert.AreEqual($"{testHost}rest/api/2/component/{testComponentId}", component.Self, nameof(component.Self));
        Assert.AreEqual(testComponentId, component.Id, nameof(component.Id));
        Assert.AreEqual(testComponentName, component.Name, nameof(component.Name));
        Assert.AreEqual("Application Description", component.Description, nameof(component.Description));
        Assert.AreEqual(testProjectKey, component.ProjectKey, nameof(component.ProjectKey));
        Assert.AreEqual(testProjectId, component.ProjectId, nameof(component.ProjectId));
    }

    [TestMethod]
    public async Task TestMethodGetProjectComponentsAsync()
    {
        using var jira = new Jira(storeKey);

        var project = await jira.GetProjectByKeyAsync(testProjectKey);
        Assert.IsNotNull(project);

        var res = await project.GetComponentsAsync();
        Assert.IsNotNull(res);

        var components = res?.ToList();
        Assert.IsNotNull(components);

        var component = components.FirstOrDefault();
        Assert.IsNotNull(component);

        Assert.AreEqual($"{testHost}rest/api/2/component/{testComponentId}", component.Self, nameof(component.Self));
        Assert.AreEqual(testComponentId, component.Id, nameof(component.Id));
        Assert.AreEqual(testComponentName, component.Name, nameof(component.Name));
        Assert.AreEqual("Application Description", component.Description, nameof(component.Description));
        Assert.AreEqual(testProjectKey, component.ProjectKey, nameof(component.ProjectKey));
        Assert.AreEqual(testProjectId, component.ProjectId, nameof(component.ProjectId));
    }

    [TestMethod]
    public async Task TestMethodGetComponentAsync()
    {
        using var jira = new Jira(storeKey);

        var component = await jira.GetComponentAsync(testComponentId);
        Assert.IsNotNull(component);

        Assert.AreEqual($"{testHost}rest/api/2/component/{testComponentId}", component.Self, nameof(component.Self));
        Assert.AreEqual(testComponentId, component.Id, nameof(component.Id));
        Assert.AreEqual(testComponentName, component.Name, nameof(component.Name));
        Assert.AreEqual("Application Description", component.Description, nameof(component.Description));
        Assert.AreEqual(testProjectKey, component.ProjectKey, nameof(component.ProjectKey));
        Assert.AreEqual(testProjectId, component.ProjectId, nameof(component.ProjectId));
    }
}
