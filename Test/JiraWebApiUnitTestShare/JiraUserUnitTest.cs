namespace JiraWebApiUnitTest;

[TestClass]
public class JiraUserUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var user = await jira.GetUserAsync(testUserKey);

        Assert.IsNotNull(user);
        Assert.AreEqual(new Uri(apiUri, $"user?username={testUserKey}"), user.Self, nameof(user.Self));
        Assert.AreEqual(testUserKey, user.Key, nameof(user.Key));
        Assert.AreEqual(testUserKey, user.Name, nameof(user.Name));
        Assert.AreEqual(testUserEmail, user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?size=xsmall&ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl16, nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?size=small&ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl24, nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?size=medium&ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl32, nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl48, nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual(testUserDisplayName, user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }

    [TestMethod]
    public async Task TestMethodGetCurrentUserAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var user = await jira.GetCurrentUserAsync();

        Assert.IsNotNull(user);
        Assert.AreEqual(new Uri(apiUri, $"user?username={testUserKey}"), user.Self, nameof(user.Self));
        Assert.AreEqual(testUserKey, user.Key, nameof(user.Key));
        Assert.AreEqual(testUserKey, user.Name, nameof(user.Name));
        Assert.AreEqual(testUserEmail, user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?size=xsmall&ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl16, nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?size=small&ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl24, nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?size=medium&ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl32, nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual(new Uri(baseUri, $"secure/useravatar?ownerId={testUserKey}&avatarId=10209"), user.AvatarUrls?.AvatarUrl48, nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual(testUserDisplayName, user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }
}
