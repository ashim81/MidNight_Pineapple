using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AudioStressTest
{
    [UnityTest]
    public IEnumerator AlarmStressTest_InScene()
    {
        //loads Scene
        SceneManager.LoadScene("TL5_TestCase");
        yield return new WaitForSeconds(1f);

        //finds the necessary objects already on scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject audioObj = GameObject.Find("AudioEngine");
        NoiseMaker original = GameObject.FindObjectOfType<NoiseMaker>();

        //Spawn a lot of NoiseMakers for stress test
        int count = 30;

        for (int i = 0; i < count; i++)
        {
            GameObject alarm = Object.Instantiate(original.gameObject);
            alarm.transform.position = player.transform.position; // place the objects on the player so trigger happens immediately
            yield return new WaitForSeconds(0.3f); // delay each trigger
        }

        // wait for triggers and coroutines
        yield return new WaitForSeconds(10f);

        // check for errors
        LogAssert.NoUnexpectedReceived();
        Assert.Pass();
    }
}
