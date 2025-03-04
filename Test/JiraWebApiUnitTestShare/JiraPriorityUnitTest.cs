﻿namespace JiraWebApiUnitTest;

[TestClass]
public class JiraPriorityUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetPrioritiesAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var list = await jira.GetPrioritiesAsync();

        var priorities = list?.ToList();
        Assert.IsNotNull(priorities);

        var priority = priorities.FirstOrDefault();
        Assert.IsNotNull(priority);
        Assert.AreEqual(new Uri(apiUri, "priority/1"), priority.Self,  nameof(priority.Self));
        Assert.AreEqual(1, priority.Id, nameof(priority.Id));
        Assert.AreEqual("Blocker", priority.Name, nameof(priority.Name));
        Assert.AreEqual("Blocks development and/or testing work, production could not run.", priority.Description, nameof(priority.Description));
        Assert.AreEqual(new Uri(baseUri, "images/icons/priorities/blocker.svg"), priority.IconUrl, nameof(priority.IconUrl));
        Assert.AreEqual("#cc0000", priority.StatusColor, nameof(priority.StatusColor));
    }

    [TestMethod]
    public async Task TestMethodGetPriorityAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var priority = await jira.GetPriorityAsync(1);

        Assert.IsNotNull(priority);
        Assert.AreEqual(new Uri(apiUri, "priority/1"), priority.Self, nameof(priority.Self));
        Assert.AreEqual(1, priority.Id, nameof(priority.Id));
        Assert.AreEqual("Blocker", priority.Name, nameof(priority.Name));
        Assert.AreEqual("Blocks development and/or testing work, production could not run.", priority.Description, nameof(priority.Description));
        Assert.AreEqual(new Uri(baseUri, "images/icons/priorities/blocker.svg"), priority.IconUrl, nameof(priority.IconUrl));
        Assert.AreEqual("#cc0000", priority.StatusColor, nameof(priority.StatusColor));
    }

    //[TestMethod]
    //public async Task TestMethodGetPrioritiesPagedAsync()
    //{
    //    using var jira = new Jira(storeKey, appName);

    //    var list = jira.GetPrioritiesPagedAsync();
    //    Assert.IsNotNull(list);

    //    var priorities = await list.ToListAsync();
    //    Assert.IsNotNull(priorities);

    //    Assert.AreEqual(0, priorities.Count, nameof(priorities.Count));
    //}
}
