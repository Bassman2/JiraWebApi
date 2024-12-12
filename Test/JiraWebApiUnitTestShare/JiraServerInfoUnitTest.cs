namespace JiraWebApiUnitTest;

[TestClass]
public class JiraServerInfoUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetServerInfoAsync()
    {
        using var jira = new Jira(storeKey);

        var serverInfo = await jira.GetServerInfoAsync();

        Assert.IsNotNull(serverInfo);
        Assert.IsNotNull(serverInfo.BuildDate);
        Assert.IsNotNull(serverInfo.ServerTime);

        Assert.AreEqual(baseUri, serverInfo.BaseUrl, nameof(serverInfo.BaseUrl));
        Assert.AreEqual(new Version(9,12,16), serverInfo.Version, nameof(serverInfo.Version));
        Assert.AreEqual("Server", serverInfo.DeploymentType, nameof(serverInfo.DeploymentType));
        Assert.AreEqual(new DateTime(2024, 12, 4), serverInfo.BuildDate, nameof(serverInfo.BuildDate));
        Assert.AreEqual(DateTime.Now.Date, serverInfo.ServerTime?.Date, nameof(serverInfo.ServerTime));
        Assert.AreEqual("6bee0863f3e6dbb91e4be2d992a3b6761c21c9e0", serverInfo.ScmInfo, nameof(serverInfo.ScmInfo));
        Assert.AreEqual("EB external Jira", serverInfo.ServerTitle, nameof(serverInfo.ServerTitle));
    }
}
