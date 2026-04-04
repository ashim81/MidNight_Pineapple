using NUnit.Framework;
using UnityEngine;

public class AudioBoundaryTest
{
    //------------Test 1:Boundary Behavior------------//
    [Test] 
    public void MasterVolume_BoundaryValues_ClampCorrectly()
    {
        GameObject obj = new GameObject("AudioEngine");
        AudioEngine engine = obj.AddComponent<AudioEngine>();

        // Below minimum
        engine.SetMasterVolume(-0.1f);
        Assert.AreEqual(0f, engine.masterVolume);

        // Minimum
        engine.SetMasterVolume(0f);
        Assert.AreEqual(0f, engine.masterVolume);

        // Maximum
        engine.SetMasterVolume(1f);
        Assert.AreEqual(1f, engine.masterVolume);

        // Above maximum
        engine.SetMasterVolume(1.1f);
        Assert.AreEqual(1f, engine.masterVolume);
    }


    //------------Test 2:Calculation------------//
    //Final volume = master * sfX
    [Test]
    public void FinalSFXVolume_UsesMasterAndSFXCorrectly()
    {
        GameObject obj = new GameObject("AudioEngine");
        AudioEngine engine = obj.AddComponent<AudioEngine>();

        engine.SetMasterVolume(0.5f);
        engine.SetSFXVolume(0.5f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.25f, result, 0.001f);
    }
}