using NUnit.Framework;
using UnityEngine;

public class VisualDetectorEditTests
{
    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(65535)]
    [TestCase(100000)]
    
    public void TrianglesInCone_ScalesCorrectly(int triangleCount)
    {
        GameObject go = new GameObject();
        VisualDetector detector = go.AddComponent<VisualDetector>();
        
        detector.trianglesInCone = triangleCount;
    
    // Manually Refresh out of Playmode
        detector.RefreshForTest();

        int expectedVertices;

        if (triangleCount > 0)
        {
            // +2 for Start & End Vertices
            expectedVertices = triangleCount + 2;
        }

        else
        {
            expectedVertices = 0;
        }

        Assert.AreEqual(expectedVertices, detector.GetVertexCount(), $"Failed at {triangleCount} triangles.");

        Object.DestroyImmediate(go);
    }
}