namespace JiraWebApiUnitTest;

[TestClass]
public class JiraProjectUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetProjectsAsync()
    {
        using var jira = new Jira(storeKey);

        var res = await jira.GetProjectsAsync();

        var projects = res?.ToList();
        Assert.IsNotNull(projects);

        var project = projects.FirstOrDefault(p => p.Key == testProjectKey);
        Assert.IsNotNull(project);
        Assert.AreEqual($"{testHost}rest/api/2/project/25411", project.Self, nameof(project.Self));
        Assert.AreEqual("25411", project.Id, nameof(project.Id));
        Assert.AreEqual("Navigation Suite", project.Name, nameof(project.Name));
        Assert.AreEqual("NAVSUITE", project.Key, nameof(project.Key));
        Assert.AreEqual(null, project.Description, nameof(project.Description));
        Assert.AreEqual(null, project.Url?.ToString(), nameof(project.Url));
        Assert.AreEqual(null, project.Email, nameof(project.Email));
        Assert.AreEqual(null, project.AssigneeType, nameof(project.AssigneeType));
    }

    [TestMethod]
    public async Task TestMethodGetProjectAsync()
    {
        using var jira = new Jira(storeKey);

        var project = await jira.GetProjectByKeyAsync(testProjectKey);

        Assert.IsNotNull(project);
        Assert.AreEqual($"{testHost}rest/api/2/project/25411", project.Self, nameof(project.Self));
        Assert.AreEqual("25411", project.Id, nameof(project.Id));
        Assert.AreEqual("Navigation Suite", project.Name, nameof(project.Name));
        Assert.AreEqual("NAVSUITE", project.Key, nameof(project.Key));
        Assert.AreEqual("Test Project", project.Description, nameof(project.Description));
        Assert.AreEqual($"{testHost}projects/NAVSUITE/issues", project.Url?.ToString(), nameof(project.Url));
        Assert.AreEqual(null, project.Email, nameof(project.Email));
        Assert.AreEqual("PROJECT_LEAD", project.AssigneeType, nameof(project.AssigneeType));
    }
}
