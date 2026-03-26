using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCollision
{
    [SetUp]
    public void Setup()
    {
        // Load the test scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level1_WitchHouse");
    }
    [UnityTest]
    public IEnumerator TestCollisionWithWalls()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(-2, -0.1f, 0);
        yield return null;
        Assert.AreNotEqual(new Vector3(-2, -0.1f, 0), player.transform.position);
    }
}
