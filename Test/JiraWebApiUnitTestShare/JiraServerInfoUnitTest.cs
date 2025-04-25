namespace JiraWebApiUnitTest;

[TestClass]
public class JiraServerInfoUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetServerInfoAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var serverInfo = await jira.GetServerInfoAsync();

        Assert.IsNotNull(serverInfo);
        Assert.IsNotNull(serverInfo.BuildDate);
        Assert.IsNotNull(serverInfo.ServerTime);

        Assert.AreEqual(baseUri, serverInfo.BaseUrl, nameof(serverInfo.BaseUrl));
        Assert.AreEqual(new Version(9,12,22), serverInfo.Version, nameof(serverInfo.Version));
        Assert.AreEqual("Server", serverInfo.DeploymentType, nameof(serverInfo.DeploymentType));
        Assert.AreEqual(new DateTime(2025, 4, 9), serverInfo.BuildDate, nameof(serverInfo.BuildDate));
        Assert.AreEqual(DateTime.Now.Date, serverInfo.ServerTime?.Date, nameof(serverInfo.ServerTime));
        Assert.AreEqual("231fbc3c75fa86a561ac9db4a71074080d297458", serverInfo.ScmInfo, nameof(serverInfo.ScmInfo));
        Assert.IsNotNull(serverInfo.ServerTitle, nameof(serverInfo.ServerTitle));
    }
}
