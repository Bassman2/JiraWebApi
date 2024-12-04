namespace JiraWebApiUnitTest;

[TestClass]
public class JiraUserUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var jira = new Jira(host, apikey);

        var user = await jira.GetUserAsync(username);

        Assert.IsNotNull(user);
        Assert.AreEqual($"{hostPath}rest/api/2/user?username={username}", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual(username, user.Key, nameof(user.Key));
        Assert.AreEqual(username, user.Name, nameof(user.Name));
        Assert.AreEqual("Ralf.Beckers@elektrobit.com", user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual($"{hostPath}secure/useravatar?size=xsmall&ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual($"{hostPath}secure/useravatar?size=small&ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual($"{hostPath}secure/useravatar?size=medium&ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual($"{hostPath}secure/useravatar?ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual("Beckers, Ralf", user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }

    [TestMethod]
    public async Task TestMethodGetCurrentUserAsync()
    {
        using var jira = new Jira(host, apikey);

        var user = await jira.GetCurrentUserAsync();

        Assert.IsNotNull(user);
        Assert.AreEqual($"{hostPath}rest/api/2/user?username={username}", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual(username, user.Key, nameof(user.Key));
        Assert.AreEqual(username, user.Name, nameof(user.Name));
        Assert.AreEqual("Ralf.Beckers@elektrobit.com", user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual($"{hostPath}secure/useravatar?size=xsmall&ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual($"{hostPath}secure/useravatar?size=small&ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual($"{hostPath}secure/useravatar?size=medium&ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual($"{hostPath}secure/useravatar?ownerId={username}&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual("Beckers, Ralf", user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }
}
