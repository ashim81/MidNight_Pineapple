using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AsimCombatStressTest
{
    [UnityTest]
    public IEnumerator CubeStressTestInScene()
    {
        // 🎮 Load your real scene
        SceneManager.LoadScene("Combat_test");

        // wait for scene to fully load
        yield return new WaitForSeconds(1f);

        // use existing Main Camera
        Camera cam = Camera.main;
        Assert.IsNotNull(cam, "Main Camera not found!");

        // spawn 100 cubes in your scene
        for (int i = 0; i < 100; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.position = new Vector3(
                Random.Range(-5f, 5f),
                Random.Range(2f, 6f),
                0
            );

            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.useGravity = true;

            yield return new WaitForSeconds(0.05f);
        }

        // keep scene running for recording
        yield return new WaitForSeconds(10f);

        Assert.Pass();
    }
}