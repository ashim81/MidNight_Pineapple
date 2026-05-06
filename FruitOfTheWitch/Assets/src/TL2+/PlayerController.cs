using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // state machine
    private InternalStateMachine stateMachine;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector2 respawnPoint;

    [SerializeField]
    private int maxExhaustion = 1500;
    public NoiseMaker noiseMaker;
    
    private int exhaustion;

    [SerializeField]
    private int maxHealth = 100;
    private int health = 100;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField] 
    private StaminaBar staminabar; 
    

    // Component
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator; //tl5: aded for animation

    // values
    private Vector2 inputVector;

    void Awake()
    {
        staminabar.SetMaxStamina(maxExhaustion);
        exhaustion = maxExhaustion;
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new InternalStateMachine();
        animator = GetComponent<Animator>(); //tl5: added for animation
        sr = GetComponent<SpriteRenderer>();
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleStealth();
        HandleExhaustion();
        HandleHealth();
        HandleAnimation();
        HandlePowerUp();
    }

    // Movement Playerside
    private void HandleMovement()
    {
        moveSpeed = stateMachine.getMoveSpeed();
        rb.linearVelocity = moveSpeed * inputVector;
    }
    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    // Stealth Playerside
    private void HandleStealth()
    {
        // tl3 stuff. THey should uncomment this
        noiseMaker.setRadius(stateMachine.getSoundRadius());
        // Debug.Log("Sneaking");

    }

    public bool isSneaky()
    {
        return stateMachine.isSneaky();
    }

    public void OnCrouch(InputValue value)
    {
        stateMachine.toggleSneakCommand.Execute();
    }

    // Sprinting
    public bool isRunning()
    {
        return stateMachine.isRunning();
    }
    private void HandleExhaustion()
    {
        exhaustion += stateMachine.getStaminaCost();
        // Debug.Log("Exhaustion: " + exhaustion);
        staminabar.SetStamina(exhaustion);
        if (exhaustion <= 0)
        {
            stateMachine.stopRunningCommand.Execute();
        } if (exhaustion >= maxExhaustion)
        {
            exhaustion = maxExhaustion;
            stateMachine.stopExhaustedCommand.Execute();
        }
    }

    public void OnSprint(InputValue value)
    {
        stateMachine.toggleRunningCommand.Execute();
    }


    private void HandleHealth()
    {
        if (health <= 0)
        {
            Respawn();
        }
    }

    private void HandleAnimation()
    {
        //tl5: added for animation
        animator.SetBool("IsMoving", inputVector.magnitude > 0.1f);
        animator.SetFloat("MoveX", inputVector.x);
        animator.SetFloat("MoveY", inputVector.y);

        
        if (inputVector.x > 0.1f)
        {
            sr.flipX = false;
        }
        else if (inputVector.x < -0.1f)
        {
            sr.flipX = true;
        }
    }

    // Power Up
    public void HandlePowerUp()
    {
        if (stateMachine.isPowered())
        {
            healthBar.SetPowered(true);
            staminabar.SetPowered(true);
        } else
        {
            healthBar.SetPowered(false);
            staminabar.SetPowered(false);
        }
    }

    public void PowerUp()
    {
        exhaustion = maxExhaustion;
        stateMachine.powerUpCommand.Execute();
    }

    public void OnCheat(InputValue value)
    {
        PowerUp();
    }

    // Wrappers

    public int getExhaustion()
    {
        return exhaustion;
    }

    public bool isExhausted()
    {
        return stateMachine.getCurrentStateEnum() == InternalStateMachine.StateEnum.Exhausted;
    }

    public int getHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        
        if (!LevelManager.instance.IsBCMode && !stateMachine.isPowered())
        {
            health -= damage;
        }

        healthBar.SetHealth(health);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        health = maxHealth;
        exhaustion = maxExhaustion/2;
        stateMachine.resetCommand.Execute();
        healthBar.SetHealth(health);
    }

    public void ThrowPunchAnimation()
    {
        animator.SetTrigger("Punch");
    }


    // Testing Methods
    public InternalStateMachine getStateMachine()
    {
        return stateMachine;
    }

    public void ForceState(InternalStateMachine.StateEnum state)
    {
        stateMachine.ForceState(state);
    }

    
}
