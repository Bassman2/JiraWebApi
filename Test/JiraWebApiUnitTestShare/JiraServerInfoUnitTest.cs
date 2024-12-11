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
        Assert.AreEqual(testHost, serverInfo.BaseUrl.ToString(), nameof(serverInfo.BaseUrl));
        Assert.AreEqual("9.12.16", serverInfo.Version.ToString(), nameof(serverInfo.Version));
        Assert.AreEqual("Server", serverInfo.DeploymentType, nameof(serverInfo.DeploymentType));
        Assert.AreEqual("04.12.2024", serverInfo.BuildDate.ToShortDateString(), nameof(serverInfo.BuildDate));
        Assert.AreEqual(DateTime.Now.ToShortDateString(), serverInfo.ServerTime.ToShortDateString(), nameof(serverInfo.ServerTime));
        Assert.AreEqual("6bee0863f3e6dbb91e4be2d992a3b6761c21c9e0", serverInfo.ScmInfo, nameof(serverInfo.ScmInfo));
        Assert.AreEqual("EB external Jira", serverInfo.ServerTitle, nameof(serverInfo.ServerTitle));
    }
}
