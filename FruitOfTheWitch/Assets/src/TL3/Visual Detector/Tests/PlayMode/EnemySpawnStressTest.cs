using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemySpawnStressTest
{
    [UnityTest]
    public IEnumerator SpawnStressTest()
    {
        int enemyCount = 10;           // starting number of enemies
        int maxEnemies = 100_000;      // optional safety limit
        bool crashed = false;

        GameObject player = new GameObject("Player");
        player.tag = "Player";

        while (!crashed && enemyCount <= maxEnemies)
        {
            bool failed = false;
            GameObject[] spawnedEnemies = new GameObject[enemyCount];

            try
            {
                // Spawn enemies
                for (int i = 0; i < enemyCount; i++)
                {
                    GameObject go = new GameObject($"Enemy_{i}");
                    go.AddComponent<VisualDetector>();
                    go.AddComponent<MeshFilter>();
                    go.AddComponent<MeshRenderer>();
                    spawnedEnemies[i] = go;
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

            // Wait a frame so Unity can process
            yield return null;

            // Clean up all enemies
            foreach (var e in spawnedEnemies)
            {
                if (e != null) GameObject.Destroy(e);
            }

            // Double the count for next iteration
            enemyCount *= 2;
        }

        // Clean up player
        GameObject.Destroy(player);

        yield return null;
    }
}