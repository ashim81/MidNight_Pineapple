using UnityEngine;

public class Enemy_AI2 : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private int health = 100;
    private bool isKnockedBack = false;

    [SerializeField]
    private WitchHealth witchhealth;

    public FireSpell fireSpell;

    private BaseBossAttack currentPhase;
    private Phase1Attack phase1;
    private Phase2Attack phase2;
    private Phase3Attack phase3;
    private bool isBoss = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        witchhealth.SetMaxHealth(health);

        phase1 = GetComponent<Phase1Attack>();
        phase2 = GetComponent<Phase2Attack>();
        phase3 = GetComponent<Phase3Attack>();

        if (phase1 != null && phase2 != null && phase3 != null)
        {
            isBoss = true;
            phase1.Initialize(player);
            phase2.Initialize(player);
            phase3.Initialize(player);
            SetPhase(phase1);
        }
    }

    void Update()
    {
        if (isBoss)
        {
            currentPhase?.ExecuteAttack(transform);
            CheckPhaseTransition();
        }
        else
        {
            fireSpell?.TryCast(transform, player);
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;
        if (isKnockedBack)
        {
            isKnockedBack = false;
            return;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }


   private void CheckPhaseTransition()
    {
    // CORRECT
    float healthPercent = (float)health / 100f * 100f;
    // ADD THIS to see in Console
    Debug.Log("Health: " + health + " | Percent: " + healthPercent + " | isBoss: " + isBoss);

    if (healthPercent > 66f && currentPhase != phase1)
        SetPhase(phase1);
    else if (healthPercent <= 66f && healthPercent > 33f && currentPhase != phase2)
        SetPhase(phase2);
    else if (healthPercent <= 33f && currentPhase != phase3)
        SetPhase(phase3);
    }

    private void SetPhase(BaseBossAttack newPhase)
    {
        currentPhase?.OnPhaseExit();
        currentPhase = newPhase;
        currentPhase.OnPhaseEnter();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            if (isBoss)
            {
                currentPhase?.OnPhaseExit();
                Debug.Log("Boss defeated!");
            }
        }
        witchhealth.SetHealth(health);
        if (isBoss) CheckPhaseTransition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball")) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            TakeDamage(7);
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(3);
            Vector2 dir = (transform.position - collision.transform.position).normalized;
            rb.AddForce(dir * 90f, ForceMode2D.Impulse);
            isKnockedBack = true;
        }
    }
}