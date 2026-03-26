using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AsimCombatStressTest
{
    [UnityTest]
    public IEnumerator CubeStressTest()
    {
        // create camera so you can SEE things
        new GameObject("Camera").AddComponent<Camera>();

        // spawn 100 cubes
        for (int i = 0; i < 100; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // random position (spread them out)
            cube.transform.position = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(0f, 10f),
                0
            );

            // optional: add gravity (makes it look cool)
            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.useGravity = true;

            yield return new WaitForSeconds(0.05f); // small delay for visual effect
        }

        // keep scene running for video
        yield return new WaitForSeconds(10f);

        Assert.Pass();
    }
}