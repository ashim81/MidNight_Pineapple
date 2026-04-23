using NUnit.Framework;

public class HiddenLevelSceneNameEditTests
{
    [Test]
    public void HiddenLevelSceneName_IsCorrect()
    {
        string expectedSceneName = "HiddenLevel";
        Assert.AreEqual("HiddenLevel", expectedSceneName);
    }
}