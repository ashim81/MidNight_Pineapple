using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TriangleBoundryTest
{
    [UnityTest]
    public IEnumerator TriangleStressTest()
    {
        // Create a temporary GameObject with VisualDetector
        GameObject go = new GameObject("Enemy");
        VisualDetector detector = go.AddComponent<VisualDetector>();

        // Make sure the mesh filter/renderer exist
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();

        int triangles = 10;        
        int maxTriangles = 100000; // safe upper limit
        bool crashed = false;

        while (!crashed && triangles <= maxTriangles)
        {
            bool failed = false;

            // Try generating mesh
            try
            {
                detector.TrianglesInCone = triangles;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Mesh failed at {triangles} triangles: {e.Message}");
                failed = true;
            }

            // If it crashed, break
            if (failed)
            {
                crashed = true;
                break;
            }

            Debug.Log($"Mesh generated successfully with {triangles} triangles");

            // Wait a frame so Unity can update
            yield return null;

            // Increase triangles
            triangles *= 2;
        }

        // Clean up
        GameObject.Destroy(go);
        yield return null;
    }
}