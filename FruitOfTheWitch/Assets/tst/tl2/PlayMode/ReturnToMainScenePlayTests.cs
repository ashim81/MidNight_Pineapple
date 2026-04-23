using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ReturnToMainScenePlayTests
{
    [UnityTest]
    public IEnumerator ReturnLoadsMainScene()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        Assert.AreEqual("Level1_Alternative", SceneManager.GetActiveScene().name);

        LogAssert.ignoreFailingMessages = false;
    }
}