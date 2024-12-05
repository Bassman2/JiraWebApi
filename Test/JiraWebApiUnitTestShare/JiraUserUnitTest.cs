using WebServiceClient.Store;

namespace JiraWebApiUnitTest;

[TestClass]
public class JiraUserUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var user = await jira.GetUserAsync(login);

        Assert.IsNotNull(user);
        Assert.AreEqual($"{host}rest/api/2/user?username={login}", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual(login, user.Key, nameof(user.Key));
        Assert.AreEqual(login, user.Name, nameof(user.Name));
        Assert.AreEqual(email, user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual($"{host}secure/useravatar?size=xsmall&ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual($"{host}secure/useravatar?size=small&ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual($"{host}secure/useravatar?size=medium&ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual($"{host}secure/useravatar?ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual(name, user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }

    [TestMethod]
    public async Task TestMethodGetCurrentUserAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var user = await jira.GetCurrentUserAsync();

        Assert.IsNotNull(user);
        Assert.AreEqual($"{host}rest/api/2/user?username={login}", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual(login, user.Key, nameof(user.Key));
        Assert.AreEqual(login, user.Name, nameof(user.Name));
        Assert.AreEqual(email, user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual($"{host}secure/useravatar?size=xsmall&ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual($"{host}secure/useravatar?size=small&ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual($"{host}secure/useravatar?size=medium&ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual($"{host}secure/useravatar?ownerId={login}&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual(name, user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }
}
