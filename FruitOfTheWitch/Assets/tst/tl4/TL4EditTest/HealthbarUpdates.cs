using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTests
{
    private GameObject healthBarObject;
    private HealthBar healthBar;
    private Slider slider;

    [SetUp]
    public void Setup()
    {
        healthBarObject = new GameObject();
        healthBar = healthBarObject.AddComponent<HealthBar>();

        GameObject sliderObject = new GameObject();
        slider = sliderObject.AddComponent<Slider>();

        typeof(HealthBar)
            .GetField("slider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(healthBar, slider);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(healthBarObject);
        Object.DestroyImmediate(slider.gameObject);
    }

    [Test]
    public void SetMaxHealth_InitializesSliderCorrectly()
    {
        healthBar.SetMaxHealth(100);
        Assert.AreEqual(100, slider.maxValue);
        Assert.AreEqual(100, slider.value);
    }
    [Test]
    public void SetMaxHealth_SetsCurrentValueToMax()
    {
        healthBar.SetMaxHealth(100);
        Assert.AreEqual(100, slider.value);
    }
    [Test]
    public void SetHealth_UpdatesSliderValueCorrectly()
    {
        healthBar.SetMaxHealth(100);
        healthBar.SetHealth(50);
        Assert.AreEqual(50, slider.value);
    }
    [Test]
    public void SetHealth_AllowsZeroHealth()
    {
        healthBar.SetMaxHealth(100);
        healthBar.SetHealth(0);
        Assert.AreEqual(0, slider.value);
    }
    [Test]
    public void SetHealth_DoesNotChangeMaxValue()
    {
        healthBar.SetMaxHealth(100);
        float originalMax = slider.maxValue;
        healthBar.SetHealth(50);
        Assert.AreEqual(originalMax, slider.maxValue);
    }
    [Test]
    public void SetHealth_DoesntChangeMaxValue()
    {
        healthBar.SetMaxHealth(100);
        healthBar.SetHealth(200);
        Assert.AreEqual(100, slider.value);
    }
}
