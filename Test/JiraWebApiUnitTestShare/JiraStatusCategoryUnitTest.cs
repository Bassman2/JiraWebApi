namespace JiraWebApiUnitTest;

[TestClass]
public class JiraStatusCategoryUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetStatusCategoriesAsync()
    {
        using var jira = new Jira(storeKey);

        var list = await jira.GetStatusCategoriesAsync();

        var statusCategories = list?.ToList();
        Assert.IsNotNull(statusCategories, nameof(statusCategories));

        var statusCategory = statusCategories.FirstOrDefault();

        Assert.IsNotNull(statusCategory, nameof(statusCategory));
        Assert.AreEqual(new Uri(apiUri, "statuscategory/1"), statusCategory.Self, nameof(statusCategory.Self));
        Assert.AreEqual(1, statusCategory.Id, nameof(statusCategory.Id));
        Assert.AreEqual("undefined", statusCategory.Key, nameof(statusCategory.Key));
        Assert.AreEqual("No Category", statusCategory.Name, nameof(statusCategory.Name));
        Assert.AreEqual("default", statusCategory.ColorName, nameof(statusCategory.ColorName));
    }

    [TestMethod]
    public async Task TestMethodGetStatusCategoryAsync()
    {
        using var jira = new Jira(storeKey);

        var statusCategory = await jira.GetStatusCategoryAsync(1);

        Assert.IsNotNull(statusCategory, nameof(statusCategory));
        Assert.AreEqual(new Uri(apiUri, "statuscategory/1"), statusCategory.Self, nameof(statusCategory.Self));
        Assert.AreEqual(1, statusCategory.Id, nameof(statusCategory.Id));
        Assert.AreEqual("undefined", statusCategory.Key, nameof(statusCategory.Key));
        Assert.AreEqual("No Category", statusCategory.Name, nameof(statusCategory.Name));
        Assert.AreEqual("default", statusCategory.ColorName, nameof(statusCategory.ColorName));

    }
}
