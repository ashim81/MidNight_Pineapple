using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HiddenLevelEntryPlayTests
{
    [UnityTest]
    public IEnumerator MainScene_HasHiddenLevelEntryObject()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        GameObject entry = GameObject.Find("HiddenlevelEntry");
        Assert.IsNotNull(entry, "HiddenlevelEntry was not found in Level1_Alternative.");

        LogAssert.ignoreFailingMessages = false;
    }
}