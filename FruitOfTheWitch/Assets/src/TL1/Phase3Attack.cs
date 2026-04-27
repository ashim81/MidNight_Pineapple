using UnityEngine;
public class Phase3Attack : BaseBossAttack
{
    private float fireRate = 5f;
    private float timer = 0f;
    private int fireballCount = 5;
    private float chargeCooldown = 20f;
    private float chargeTimer = 0f;
    private float chargeSpeed = 10f;

    private Rigidbody2D rb;

    // Override with the signature that accepts the rb directly
    public override void Initialize(Transform playerTransform, Rigidbody2D bossRb)
    {
        base.Initialize(playerTransform, bossRb);
        rb = bossRb;
    }

    public override void OnPhaseEnter()
    {
        Debug.Log("Phase 3 — Boss ENRAGED!");
    }

    public override void ExecuteAttack(Transform boss)
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            ShootFireballs(boss, fireballCount, 20f);
        }

        chargeTimer += Time.deltaTime;
        if (chargeTimer >= chargeCooldown)
        {
            chargeTimer = 0f;
            if (rb != null && player != null)
            {
                Vector2 dir = (player.position - boss.position).normalized;
                rb.AddForce(dir * chargeSpeed, ForceMode2D.Impulse);
                Debug.Log("Boss charges!");
            }
        }
    }

    public override void OnPhaseExit()
    {
        Debug.Log("Boss defeated!");
    }
}