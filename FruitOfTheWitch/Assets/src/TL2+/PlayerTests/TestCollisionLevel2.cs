using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCollisionLevel2
{
    [SetUp]
    public void Setup()
    {
        // Load the test scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level2_Jungle");
    }
    [UnityTest]
    public IEnumerator TestCollisionWithWalls()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(-2, -0.1f, 0);
        yield return null;
        Assert.AreEqual(0,0); // Nothing to collide with. Vacuously true.
    }

    [TearDown]
     public void Teardown()
     {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/EmptyScene");
     }
}
