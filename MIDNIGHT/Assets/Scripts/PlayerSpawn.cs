using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint is NOT assigned in PlayerSpawn script.");
            return;
        }

        transform.position = spawnPoint.position;
    }
}
