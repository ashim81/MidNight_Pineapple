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
        //  Load your real scene
        SceneManager.LoadScene("Combat_test");

        // wait for scene to fully load
        yield return new WaitForSeconds(1f);

        // use existing Main Camera
        Camera cam = Camera.main;
        Assert.IsNotNull(cam, "Main Camera not found!");

        // spawn enemies 
        for (int i = 0; i < 1500; i++)
        {
            GameObject enemy = Object.Instantiate(
                Resources.Load<GameObject>("Enemy")
            );

            enemy.transform.position = new Vector3(
                Random.Range(-5f, 5f),
                Random.Range(2f, 6f),
                0
            );
            enemy.transform.localScale = new Vector3(0.9f, 0.9f, 1.9f);

           

            yield return new WaitForSeconds(0.05f);
        }

        // keep scene running for recording
        yield return new WaitForSeconds(10f);
        float testDuration = 10f;
        float timer = 0f;

        while (timer < testDuration)
        {
            // FAIL if frame time is too high (low FPS)
            Assert.Less(
                Time.deltaTime,
                0.033f,
                $"Frame drop detected! deltaTime: {Time.deltaTime}"
            );

            timer += Time.deltaTime;
            yield return null; // wait next frame
        }

        Assert.Pass();
    }
}