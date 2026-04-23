using NUnit.Framework;
using UnityEngine;

public class AudioBoundaryTest
{
    private GameObject obj;
    private AudioEngine engine;

    [SetUp]
    public void SetUp()
    {
        obj = new GameObject("AudioEngine");
        engine = obj.AddComponent<AudioEngine>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(obj);
    }

    //------------Original Test 1: Boundary Behavior------------//
    [Test] 
    public void MasterVolume_BoundaryValues_ClampCorrectly()
    {
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

    //------------Original Test 2: Calculation------------//
    // Final volume = master * sfx
    [Test]
    public void FinalSFXVolume_UsesMasterAndSFXCorrectly()
    {
        engine.SetMasterVolume(0.5f);
        engine.SetSFXVolume(0.5f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.25f, result, 0.001f);
    }

    //------------Additional Test 3------------//
    [Test]
    public void MasterVolume_BelowMinimum_ClampsToZero()
    {
        engine.SetMasterVolume(-5f);
        Assert.AreEqual(0f, engine.masterVolume);
    }

    //------------Additional Test 4------------//
    [Test]
    public void MasterVolume_AboveMaximum_ClampsToOne()
    {
        engine.SetMasterVolume(5f);
        Assert.AreEqual(1f, engine.masterVolume);
    }

    //------------Additional Test 5------------//
    [Test]
    public void MasterVolume_ValidMiddleValue_SetsCorrectly()
    {
        engine.SetMasterVolume(0.7f);
        Assert.AreEqual(0.7f, engine.masterVolume, 0.001f);
    }

    //------------Additional Test 6------------//
    [Test]
    public void MasterVolume_ExactMinimum_RemainsZero()
    {
        engine.SetMasterVolume(0f);
        Assert.AreEqual(0f, engine.masterVolume);
    }

    //------------Additional Test 7------------//
    [Test]
    public void MasterVolume_ExactMaximum_RemainsOne()
    {
        engine.SetMasterVolume(1f);
        Assert.AreEqual(1f, engine.masterVolume);
    }

    //------------Additional Test 8------------//
    [Test]
    public void SFXVolume_BelowMinimum_ClampsToZero()
    {
        engine.SetSFXVolume(-2f);
        Assert.AreEqual(0f, engine.sfxVolume);
    }

    //------------Additional Test 9------------//
    [Test]
    public void SFXVolume_AboveMaximum_ClampsToOne()
    {
        engine.SetSFXVolume(2f);
        Assert.AreEqual(1f, engine.sfxVolume);
    }

    //------------Additional Test 10------------//
    [Test]
    public void SFXVolume_ValidMiddleValue_SetsCorrectly()
    {
        engine.SetSFXVolume(0.3f);
        Assert.AreEqual(0.3f, engine.sfxVolume, 0.001f);
    }

    //------------Additional Test 11------------//
    [Test]
    public void SFXVolume_ExactMinimum_RemainsZero()
    {
        engine.SetSFXVolume(0f);
        Assert.AreEqual(0f, engine.sfxVolume);
    }

    //------------Additional Test 12------------//
    [Test]
    public void SFXVolume_ExactMaximum_RemainsOne()
    {
        engine.SetSFXVolume(1f);
        Assert.AreEqual(1f, engine.sfxVolume);
    }

    //------------Additional Test 13------------//
    [Test]
    public void FinalSFXVolume_MasterZero_ReturnsZero()
    {
        engine.SetMasterVolume(0f);
        engine.SetSFXVolume(0.8f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0f, result, 0.001f);
    }

    //------------Additional Test 14------------//
    [Test]
    public void FinalSFXVolume_SFXZero_ReturnsZero()
    {
        engine.SetMasterVolume(0.8f);
        engine.SetSFXVolume(0f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0f, result, 0.001f);
    }

    //------------Additional Test 15------------//
    [Test]
    public void FinalSFXVolume_BothZero_ReturnsZero()
    {
        engine.SetMasterVolume(0f);
        engine.SetSFXVolume(0f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0f, result, 0.001f);
    }

    //------------Additional Test 16------------//
    [Test]
    public void FinalSFXVolume_BothOne_ReturnsOne()
    {
        engine.SetMasterVolume(1f);
        engine.SetSFXVolume(1f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(1f, result, 0.001f);
    }

    //------------Additional Test 17------------//
    [Test]
    public void FinalSFXVolume_MasterOne_UsesOnlySFXValue()
    {
        engine.SetMasterVolume(1f);
        engine.SetSFXVolume(0.4f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.4f, result, 0.001f);
    }

    //------------Additional Test 18------------//
    [Test]
    public void FinalSFXVolume_SFXOne_UsesOnlyMasterValue()
    {
        engine.SetMasterVolume(0.6f);
        engine.SetSFXVolume(1f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.6f, result, 0.001f);
    }

    //------------Additional Test 19------------//
    [Test]
    public void FinalSFXVolume_QuarterAndHalf_ReturnsCorrectValue()
    {
        engine.SetMasterVolume(0.25f);
        engine.SetSFXVolume(0.5f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.125f, result, 0.001f);
    }

    //------------Additional Test 20------------//
    [Test]
    public void FinalSFXVolume_ThreeQuartersAndHalf_ReturnsCorrectValue()
    {
        engine.SetMasterVolume(0.75f);
        engine.SetSFXVolume(0.5f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.375f, result, 0.001f);
    }

    //------------Additional Test 21------------//
    [Test]
    public void FinalSFXVolume_OneTenthAndOneTenth_ReturnsCorrectValue()
    {
        engine.SetMasterVolume(0.1f);
        engine.SetSFXVolume(0.1f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.01f, result, 0.001f);
    }

    //------------Additional Test 22------------//
    [Test]
    public void FinalSFXVolume_AfterChangingMaster_UpdatesCorrectly()
    {
        engine.SetMasterVolume(0.5f);
        engine.SetSFXVolume(0.5f);
        engine.SetMasterVolume(0.8f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.4f, result, 0.001f);
    }

    //------------Additional Test 23------------//
    [Test]
    public void FinalSFXVolume_AfterChangingSFX_UpdatesCorrectly()
    {
        engine.SetMasterVolume(0.5f);
        engine.SetSFXVolume(0.5f);
        engine.SetSFXVolume(0.2f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.1f, result, 0.001f);
    }

    //------------Additional Test 24------------//
    [Test]
    public void FinalSFXVolume_MultipleChanges_UsesLatestValues()
    {
        engine.SetMasterVolume(0.2f);
        engine.SetSFXVolume(0.9f);
        engine.SetMasterVolume(0.6f);
        engine.SetSFXVolume(0.4f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.24f, result, 0.001f);
    }

    //------------Additional Test 25------------//
    [Test]
    public void FinalSFXVolume_WhenMasterWasClampedLow_UsesZero()
    {
        engine.SetMasterVolume(-1f);
        engine.SetSFXVolume(0.7f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0f, result, 0.001f);
    }

    //------------Additional Test 26------------//
    [Test]
    public void FinalSFXVolume_WhenMasterWasClampedHigh_UsesOne()
    {
        engine.SetMasterVolume(2f);
        engine.SetSFXVolume(0.7f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.7f, result, 0.001f);
    }

    //------------Additional Test 27------------//
    [Test]
    public void FinalSFXVolume_WhenSFXWasClampedLow_UsesZero()
    {
        engine.SetMasterVolume(0.7f);
        engine.SetSFXVolume(-1f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0f, result, 0.001f);
    }

    //------------Additional Test 28------------//
    [Test]
    public void FinalSFXVolume_WhenSFXWasClampedHigh_UsesOne()
    {
        engine.SetMasterVolume(0.7f);
        engine.SetSFXVolume(2f);

        float result = engine.GetFinalSFXVolume();

        Assert.AreEqual(0.7f, result, 0.001f);
    }

    //------------Additional Test 29------------//
    [Test]
    public void DefaultMasterVolume_StartsAtOne()
    {
        Assert.AreEqual(1f, engine.masterVolume, 0.001f);
    }

    //------------Additional Test 30------------//
    [Test]
    public void DefaultSFXVolume_StartsAtOne()
    {
        Assert.AreEqual(1f, engine.sfxVolume, 0.001f);
    }
}