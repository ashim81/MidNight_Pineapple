using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 1f;

    private float lastAttackTime;

    public void TryAttack(Transform targetTransform)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Health targetHealth = targetTransform.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage, transform);
                lastAttackTime = Time.time;
            }
        }
    }
}