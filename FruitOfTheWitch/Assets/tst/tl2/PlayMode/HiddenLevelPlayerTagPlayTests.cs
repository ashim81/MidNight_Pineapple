using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HiddenLevelPlayerTagPlayTests
{
    [UnityTest]
    public IEnumerator Scene_ContainsPlayerObjectWithPlayerTag()
    {
        LogAssert.ignoreFailingMessages = true;

        SceneManager.LoadScene("Level1_Alternative");
        yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "No active GameObject with tag 'Player' was found in Level1_Alternative.");

        LogAssert.ignoreFailingMessages = false;
    }
}