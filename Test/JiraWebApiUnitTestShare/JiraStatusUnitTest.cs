namespace JiraWebApiUnitTest;

[TestClass]
public class JiraStatusUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetStatusesAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var list = await jira.GetStatusesAsync();

        var statuses = list?.ToList();
        Assert.IsNotNull(statuses);
        
        var status = statuses.FirstOrDefault();
        Assert.IsNotNull(status);
        
        Assert.AreEqual(new Uri(apiUri, "status/1"), status.Self, nameof(status.Self));
        Assert.AreEqual(1, status.Id, nameof(status.Id));
        Assert.AreEqual("Open", status.Name, nameof(status.Name));
        Assert.AreEqual("The issue is open and ready for the assignee to start work on it.", status.Description, nameof(status.Description));
        Assert.AreEqual(new Uri(baseUri, "images/icons/statuses/open.png"), status.IconUrl, nameof(status.IconUrl));

        Assert.IsNotNull(status.StatusCategory, nameof(status.StatusCategory));
        Assert.AreEqual(new Uri(apiUri, "statuscategory/2"), status.StatusCategory.Self, nameof(status.StatusCategory.Self));
        Assert.AreEqual(2, status.StatusCategory.Id, nameof(status.StatusCategory.Id));
        Assert.AreEqual("new", status.StatusCategory.Key, nameof(status.StatusCategory.Key));
        Assert.AreEqual("To Do", status.StatusCategory.Name, nameof(status.StatusCategory.Name));
        Assert.AreEqual("default", status.StatusCategory.ColorName, nameof(status.StatusCategory.ColorName));
    }

    [TestMethod]
    public async Task TestMethodGetStatusAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var status = await jira.GetStatusAsync(1);

        Assert.IsNotNull(status);

        Assert.AreEqual(new Uri(apiUri, "status/1"), status.Self, nameof(status.Self));
        Assert.AreEqual(1, status.Id, nameof(status.Id));
        Assert.AreEqual("Open", status.Name, nameof(status.Name));
        Assert.AreEqual("The issue is open and ready for the assignee to start work on it.", status.Description, nameof(status.Description));
        Assert.AreEqual(new Uri(baseUri, "images/icons/statuses/open.png"), status.IconUrl, nameof(status.IconUrl));

        Assert.IsNotNull(status.StatusCategory, nameof(status.StatusCategory));
        Assert.AreEqual(new Uri(apiUri, "statuscategory/2"), status.StatusCategory.Self, nameof(status.StatusCategory.Self));
        Assert.AreEqual(2, status.StatusCategory.Id, nameof(status.StatusCategory.Id));
        Assert.AreEqual("new", status.StatusCategory.Key, nameof(status.StatusCategory.Key));
        Assert.AreEqual("To Do", status.StatusCategory.Name, nameof(status.StatusCategory.Name));
        Assert.AreEqual("default", status.StatusCategory.ColorName, nameof(status.StatusCategory.ColorName));

    }

    //[TestMethod]
    //public async Task TestMethodGetStatusesPagedAsync()
    //{
    //    using var jira = new Jira(storeKey, appName);

    //    var list = jira.GetStatusesPagedAsync();
    //    Assert.IsNotNull(list);

    //    var statuses = await list.ToListAsync();
    //    Assert.IsNotNull(statuses);

    //    Assert.AreEqual(0, statuses.Count, nameof(statuses.Count));
    //}
}
