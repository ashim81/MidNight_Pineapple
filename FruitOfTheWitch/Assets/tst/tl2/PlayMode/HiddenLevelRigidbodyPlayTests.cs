using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HiddenLevelRigidbodyPlayTests
{
    [UnityTest]
    public IEnumerator HiddenLevelEntry_HasRigidbody2D()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        GameObject entry = GameObject.Find("HiddenlevelEntry");
        Assert.IsNotNull(entry, "HiddenlevelEntry was not found in Level1_Alternative.");

        Rigidbody2D rb = entry.GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rb, "HiddenlevelEntry does not have a Rigidbody2D.");

        LogAssert.ignoreFailingMessages = false;
    }
}