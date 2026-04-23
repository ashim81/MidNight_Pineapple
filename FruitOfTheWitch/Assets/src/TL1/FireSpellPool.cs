using System.Collections.Generic;
using UnityEngine;

public class FireballPool : MonoBehaviour
{
    // Singleton so everyone can access the pool
    public static FireballPool Instance;

    public GameObject fireballPrefab;

    [Tooltip("How many fireballs to create at start")]
    public int poolSize = 15; // 3 fireballs x 5 seconds worth

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Pre-warm — create all fireballs at start, put them to sleep
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fb = Instantiate(fireballPrefab);
            fb.SetActive(false); // sleeping
            pool.Enqueue(fb);
        }
    }

    // Wake up a fireball from the pool
    public GameObject Get(Vector3 position)
    {
        if (pool.Count > 0)
        {
            GameObject fb = pool.Dequeue();
            fb.transform.position = position;
            fb.SetActive(true); // wake up
            fb.GetComponent<Fireball>()?.ResetFireball();
            return fb;
        }

        // Pool is empty — create extra just in case
        GameObject newFb = Instantiate(fireballPrefab);
        return newFb;
    }

    // Put fireball back to sleep
    public void ReturnToPool(GameObject fb)
    {
        fb.SetActive(false); // sleep
        pool.Enqueue(fb);
    }
}