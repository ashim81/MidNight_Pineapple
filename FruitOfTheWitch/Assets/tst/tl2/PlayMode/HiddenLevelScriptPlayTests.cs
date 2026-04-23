using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HiddenLevelScriptPlayTests
{
    [UnityTest]
    public IEnumerator HiddenLevelEntry_HasHiddenLevelEntryScript()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        GameObject entry = GameObject.Find("HiddenlevelEntry");
        Assert.IsNotNull(entry, "HiddenlevelEntry was not found in Level1_Alternative.");

        Component script = entry.GetComponent("HiddenLevelEntry");
        Assert.IsNotNull(script, "HiddenlevelEntry does not have a component named HiddenLevelEntry attached.");

        LogAssert.ignoreFailingMessages = false;
    }
}