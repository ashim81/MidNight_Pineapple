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

    // PHASE VARIABLES
    private BaseBossAttack currentPhase;
    private Phase1Attack phase1;
    private Phase2Attack phase2;
    private Phase3Attack phase3;
    private bool isBoss = false;

    // HEALTH COMPONENT
    private Health healthComponent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        witchhealth.SetMaxHealth(health);

        // GET Health component
        healthComponent = GetComponent<Health>();

        // GET Phase components
        phase1 = GetComponent<Phase1Attack>();
        phase2 = GetComponent<Phase2Attack>();
        phase3 = GetComponent<Phase3Attack>();

        // IF all phases exist = this is a boss
        if (phase1 != null && phase2 != null && phase3 != null)
        {
            isBoss = true;
            Debug.Log("BOSS MODE ON");
            phase1.Initialize(player);
            phase2.Initialize(player);
            phase3.Initialize(player);
            SetPhase(phase1);
        }
        else
        {
            Debug.Log("NORMAL ENEMY MODE");
        }
    }

    void Update()
    {
        if (isBoss)
        {
            // Read health from Health.cs
            if (healthComponent != null)
                health = healthComponent.GetCurrentHealth();

            // Run current phase attack
            currentPhase?.ExecuteAttack(transform);

            // Check if phase should change
            CheckPhaseTransition();
        }
        else
        {
            // Normal enemy uses FireSpell
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
        // Get max health from Health.cs
        int maxHealth = healthComponent != null ? healthComponent.maxHealth : 100;
        float healthPercent = (float)health / maxHealth * 100f;

        Debug.Log("Health: " + health + " Percent: " + healthPercent + " Phase: " + currentPhase);

        if (healthPercent > 50f && currentPhase != phase1)
            SetPhase(phase1);
        else if (healthPercent <= 50f && healthPercent > 10f && currentPhase != phase2)
            SetPhase(phase2);
        else if (healthPercent <= 10f && currentPhase != phase3)
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