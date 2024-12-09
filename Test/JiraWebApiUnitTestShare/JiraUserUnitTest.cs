namespace JiraWebApiUnitTest;

[TestClass]
public class JiraUserUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var jira = new Jira(storeKey);

        var user = await jira.GetUserAsync(testUserKey);

        Assert.IsNotNull(user);
        Assert.AreEqual($"{testHost}rest/api/2/user?username={testUserKey}", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual(testUserKey, user.Key, nameof(user.Key));
        Assert.AreEqual(testUserKey, user.Name, nameof(user.Name));
        Assert.AreEqual(testUserEmail, user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual($"{testHost}secure/useravatar?size=xsmall&ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual($"{testHost}secure/useravatar?size=small&ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual($"{testHost}secure/useravatar?size=medium&ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual($"{testHost}secure/useravatar?ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual(testUserDisplayName, user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }

    [TestMethod]
    public async Task TestMethodGetCurrentUserAsync()
    {
        using var jira = new Jira(storeKey);

        var user = await jira.GetCurrentUserAsync();

        Assert.IsNotNull(user);
        Assert.AreEqual($"{testHost}rest/api/2/user?username={testUserKey}", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual(testUserKey, user.Key, nameof(user.Key));
        Assert.AreEqual(testUserKey, user.Name, nameof(user.Name));
        Assert.AreEqual(testUserEmail, user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual($"{testHost}secure/useravatar?size=xsmall&ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual($"{testHost}secure/useravatar?size=small&ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual($"{testHost}secure/useravatar?size=medium&ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual($"{testHost}secure/useravatar?ownerId={testUserKey}&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual(testUserDisplayName, user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }
}
