using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemySpawnStressTest
{
    [UnityTest]
    public IEnumerator SpawnStressTest()
    {
        int enemyCount = 10;           
        int maxEnemies = 10240;     
        
        bool crashed = false;

        while (!crashed && enemyCount <= maxEnemies)
        {
            bool failed = false;
            
            GameObject[] spawnedEnemies = new GameObject[enemyCount];

            try
            {
                // Build Enemies
                for (int i = 0; i < enemyCount; i++)
                {
                    GameObject ememyBoi = new GameObject($"Enemy_{i}");
                    ememyBoi.AddComponent<VisualDetector>();
                    spawnedEnemies[i] = ememyBoi;
                }
            }

            catch (System.Exception e)
            {
                Debug.LogError($"Spawning failed at {enemyCount} enemies: {e.Message}");
                failed = true;
            }

            if (failed)
            {
                crashed = true;
                break;
            }

            Debug.Log($"Successfully spawned {enemyCount} enemies");

            // Wait
            yield return null;

            // Cleanup
            foreach (var e in spawnedEnemies)
            {
                if (e != null) GameObject.Destroy(e);
            }

            // Double Enemies for Next Run
            enemyCount *= 2;
        }

        yield return null;
    }
}