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
        var components = res?.ToList();
        Assert.IsNotNull(components);



        //Assert.AreEqual($"{testHost}rest/api/2/project/25411", project.Self, nameof(project.Self));
        //Assert.AreEqual("25411", project.Id, nameof(project.Id));
        //Assert.AreEqual("Navigation Suite", project.Name, nameof(project.Name));
        //Assert.AreEqual("NAVSUITE", project.Key, nameof(project.Key));
        //Assert.AreEqual("Test Project", project.Description, nameof(project.Description));
        //Assert.AreEqual($"{testHost}projects/NAVSUITE/issues", project.Url?.ToString(), nameof(project.Url));
        //Assert.AreEqual(null, project.Email, nameof(project.Email));
        //Assert.AreEqual("PROJECT_LEAD", project.AssigneeType, nameof(project.AssigneeType));
    }

    [TestMethod]
    public async Task TestMethodGetProjectComponentsAsync()
    {
        using var jira = new Jira(storeKey);

        var project = await jira.GetProjectByKeyAsync(testProjectKey);
        Assert.IsNotNull(project);

        var res = await project.GetComponentsAsync();
        var components = res?.ToList();
        Assert.IsNotNull(components);



        //Assert.AreEqual($"{testHost}rest/api/2/project/25411", project.Self, nameof(project.Self));
        //Assert.AreEqual("25411", project.Id, nameof(project.Id));
        //Assert.AreEqual("Navigation Suite", project.Name, nameof(project.Name));
        //Assert.AreEqual("NAVSUITE", project.Key, nameof(project.Key));
        //Assert.AreEqual("Test Project", project.Description, nameof(project.Description));
        //Assert.AreEqual($"{testHost}projects/NAVSUITE/issues", project.Url?.ToString(), nameof(project.Url));
        //Assert.AreEqual(null, project.Email, nameof(project.Email));
        //Assert.AreEqual("PROJECT_LEAD", project.AssigneeType, nameof(project.AssigneeType));
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
        Assert.AreEqual(testProjectKey, component.ProjectKey, nameof(component.ProjectKey));
        Assert.AreEqual(testProjectId, component.ProjectId, nameof(component.ProjectId));
    }
}
