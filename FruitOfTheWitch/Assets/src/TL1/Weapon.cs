using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 20;
    public float activeTime = 0.2f;

    private Collider2D weaponCollider;

    void Awake()
    {
        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false; // start disabled
    }

    public void Activate()
    {
        weaponCollider.enabled = true;
        Invoke(nameof(Deactivate), activeTime);
    }

    void Deactivate()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    Health target = collision.GetComponent<Health>();

    if (target != null)
    {
        target.TakeDamage(damage, transform);
    }
    }
}