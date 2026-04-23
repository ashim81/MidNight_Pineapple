using NUnit.Framework;
using UnityEngine;

public class VisualDetectorEditTests
{
    [Test]
    [TestCase(0f)]
    [TestCase(10.5f)]
    [TestCase(-5f)]
    [TestCase(1)]
    [TestCase(65535)]
    [TestCase(100000)]

    public void SetRadius_UpdatesValueCorrectly(float testRadius)
    {
        GameObject go = new GameObject();
        NoiseMaker detector = go.AddComponent<NoiseMaker>();

        detector.setRadius(testRadius);

        float expectedValue = testRadius < 0 ? 0f : testRadius;

        Assert.AreEqual(expectedValue, detector.getRadius(), 
            $"Radius did not match expected value for input: {testRadius}");

        Object.DestroyImmediate(go);
    }
}