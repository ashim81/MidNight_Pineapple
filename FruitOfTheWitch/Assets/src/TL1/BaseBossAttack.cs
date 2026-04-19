using UnityEngine;

public class BaseBossAttack : MonoBehaviour
{
    protected Transform player;

    public virtual void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }

    public virtual void ExecuteAttack(Transform boss) { }
    public virtual void OnPhaseEnter() { }
    public virtual void OnPhaseExit() { }

    protected void ShootFireballs(Transform boss, int count, float spread)
    {
        Vector3 baseDir = (player.position - boss.position).normalized;
        float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;
        float startAngle = -spread * (count - 1) / 2f;

        for (int i = 0; i < count; i++)
        {
            float angle = (baseAngle + startAngle + (spread * i)) * Mathf.Deg2Rad;
            GameObject fb = FireballPool.Instance.Get(boss.position);
            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
            fb.GetComponent<Fireball>()?.SetDirection(boss.position + dir);
        }
    }
}

public class Phase1Attack : BaseBossAttack
{
    private float fireRate = 4f;
    private float timer = 0f;
    private int fireballCount = 1;

    public override void OnPhaseEnter()
    {
        Debug.Log("Phase 1 — Boss is calm");
    }

    public override void ExecuteAttack(Transform boss)
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            ShootFireballs(boss, fireballCount, 0f);
        }
    }

    public override void OnPhaseExit()
    {
        Debug.Log("Phase 1 ending!");
    }
}

public class Phase2Attack : BaseBossAttack
{
    private float fireRate = 3f;
    private float timer = 0f;
    private int fireballCount = 3;

    public override void OnPhaseEnter()
    {
        Debug.Log("Phase 2 — Boss is angry!");
    }

    public override void ExecuteAttack(Transform boss)
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            ShootFireballs(boss, fireballCount, 20f);
        }
    }

    public override void OnPhaseExit()
    {
        Debug.Log("Phase 2 ending!");
    }
}

public class Phase3Attack : BaseBossAttack
{
    private float fireRate = 1f;
    private float timer = 0f;
    private int fireballCount = 5;
    private Rigidbody2D rb;
    private float chargeCooldown = 10f;
    private float chargeTimer = 0f;
    private float chargeSpeed = 10f;

    public override void Initialize(Transform playerTransform)
    {
        base.Initialize(playerTransform);
        rb = GetComponentInParent<Rigidbody2D>();
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
            ShootFireballs(boss, fireballCount, 15f);
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