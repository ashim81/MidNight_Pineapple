using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HiddenLevelActiveStatePlayTests
{
    [UnityTest]
    public IEnumerator HiddenLevelEntry_IsActiveInScene()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        GameObject entry = GameObject.Find("HiddenlevelEntry");
        Assert.IsNotNull(entry, "HiddenlevelEntry was not found in Level1_Alternative.");

        Assert.IsTrue(entry.activeInHierarchy, "HiddenlevelEntry exists but is not active in the scene.");

        LogAssert.ignoreFailingMessages = false;
    }
}