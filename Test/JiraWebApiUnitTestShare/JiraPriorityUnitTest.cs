namespace JiraWebApiUnitTest;

[TestClass]
public class JiraPriorityUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetPrioritiesAsync()
    {
        using var jira = new Jira(storeKey);

        var list = await jira.GetPrioritiesAsync();

        var priorities = list?.ToList();
        Assert.IsNotNull(priorities);

        var priority = priorities.FirstOrDefault();
        Assert.IsNotNull(priority);
        Assert.AreEqual($"{testHost}rest/api/2/priority/1", priority.Self,  nameof(priority.Self));
        Assert.AreEqual(1, priority.Id, nameof(priority.Id));
        Assert.AreEqual("Blocker", priority.Name, nameof(priority.Name));
        Assert.AreEqual("Blocks development and/or testing work, production could not run.", priority.Description, nameof(priority.Description));
        Assert.AreEqual($"{testHost}images/icons/priorities/blocker.svg", priority.IconUrl?.ToString(), nameof(priority.IconUrl));
        Assert.AreEqual("#cc0000", priority.StatusColor, nameof(priority.StatusColor));
    }

    [TestMethod]
    public async Task TestMethodGetPriorityAsync()
    {
        using var jira = new Jira(storeKey);

        var priority = await jira.GetPriorityAsync(1);

        Assert.IsNotNull(priority);
        Assert.AreEqual($"{testHost}rest/api/2/priority/1", priority.Self, nameof(priority.Self));
        Assert.AreEqual(1, priority.Id, nameof(priority.Id));
        Assert.AreEqual("Blocker", priority.Name, nameof(priority.Name));
        Assert.AreEqual("Blocks development and/or testing work, production could not run.", priority.Description, nameof(priority.Description));
        Assert.AreEqual($"{testHost}images/icons/priorities/blocker.svg", priority.IconUrl?.ToString(), nameof(priority.IconUrl));
        Assert.AreEqual("#cc0000", priority.StatusColor, nameof(priority.StatusColor));
    }
}
