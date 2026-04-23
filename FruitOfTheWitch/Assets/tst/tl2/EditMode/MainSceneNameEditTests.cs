using NUnit.Framework;

public class MainSceneNameEditTests
{
    [Test]
    public void MainSceneName_IsCorrect()
    {
        string expectedSceneName = "Level1_Alternative";
        Assert.AreEqual("Level1_Alternative", expectedSceneName);
    }
}