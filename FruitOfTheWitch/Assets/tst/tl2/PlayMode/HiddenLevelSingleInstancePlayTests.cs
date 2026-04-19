using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HiddenLevelSingleInstancePlayTests
{
    [UnityTest]
    public IEnumerator Scene_HasOnlyOneHiddenLevelEntry()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "HiddenlevelEntry")
            {
                count++;
            }
        }

        Assert.AreEqual(1, count, $"Expected exactly 1 HiddenlevelEntry, but found {count}.");

        LogAssert.ignoreFailingMessages = false;
    }
}