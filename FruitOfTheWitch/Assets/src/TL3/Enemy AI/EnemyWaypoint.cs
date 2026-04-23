using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }
}