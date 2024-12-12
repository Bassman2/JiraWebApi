namespace JiraWebApiUnitTest;

[TestClass]
public class JiraResolutionUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetResolutionsAsync()
    {
        using var jira = new Jira(storeKey);

        var list = await jira.GetResolutionsAsync();

        var resolutions = list?.ToList();
        Assert.IsNotNull(resolutions);

        var resolution = resolutions.FirstOrDefault();
        Assert.IsNotNull(resolution);
        Assert.AreEqual(new Uri(apiUri, "resolution/1"), resolution.Self, nameof(resolution.Self));
        Assert.AreEqual(1, resolution.Id, nameof(resolution.Id));
        Assert.AreEqual("Fixed", resolution.Name, nameof(resolution.Name));
        Assert.AreEqual("A fix for this issue is checked into the tree and tested.", resolution.Description, nameof(resolution.Description));
        Assert.AreEqual(null, resolution.IconUrl, nameof(resolution.IconUrl));
    }

    [TestMethod]
    public async Task TestMethodGetResolutionAsync()
    {
        using var jira = new Jira(storeKey);

        var resolution = await jira.GetResolutionAsync(1);

        Assert.IsNotNull(resolution);
        Assert.AreEqual(new Uri(apiUri, "resolution/1"), resolution.Self, nameof(resolution.Self));
        Assert.AreEqual(1, resolution.Id, nameof(resolution.Id));
        Assert.AreEqual("Fixed", resolution.Name, nameof(resolution.Name));
        Assert.AreEqual("A fix for this issue is checked into the tree and tested.", resolution.Description, nameof(resolution.Description));
        Assert.AreEqual(null, resolution.IconUrl, nameof(resolution.IconUrl));
    }
}