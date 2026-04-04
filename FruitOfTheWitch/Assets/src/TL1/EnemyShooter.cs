using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public void SetDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Start()
    {
        Destroy(gameObject, 4f); // destroy after 4 seconds
    }
}