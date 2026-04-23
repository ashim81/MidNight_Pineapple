using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SceneReturnStressPlayTests
{
    [UnityTest]
    public IEnumerator RepeatedSceneLoads_StillReturnToMainScene()
    {
        LogAssert.ignoreFailingMessages = true;

        for (int i = 0; i < 3; i++)
        {
            SceneManager.LoadScene("Level1_Alternative");
            yield return null;

            SceneManager.LoadScene("HiddenLevel");
            yield return null;
        }

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        Assert.AreEqual("Level1_Alternative", SceneManager.GetActiveScene().name);

        GameObject entry = GameObject.Find("HiddenlevelEntry");
        Assert.IsNotNull(entry, "HiddenlevelEntry was not found after repeated scene loading.");

        LogAssert.ignoreFailingMessages = false;
    }
}