using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HiddenLevelColliderEnabledPlayTests
{
    [UnityTest]
    public IEnumerator HiddenLevelEntry_ColliderIsEnabled()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        GameObject entry = GameObject.Find("HiddenlevelEntry");
        Assert.IsNotNull(entry, "HiddenlevelEntry was not found in Level1_Alternative.");

        BoxCollider2D box = entry.GetComponent<BoxCollider2D>();
        Assert.IsNotNull(box, "HiddenlevelEntry does not have a BoxCollider2D.");

        Assert.IsTrue(box.enabled, "HiddenlevelEntry BoxCollider2D exists but is disabled.");

        LogAssert.ignoreFailingMessages = false;
    }
}