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
    private bool BCmode = false;

    [SerializeField]
    private int maxExhaustion = 1500;
    [SerializeField]
    private int exhaustionGain = 1;
    [SerializeField]
    private int exhaustionLoss = 3;
    public NoiseMaker noiseMaker;
    
    private int exhaustion;

    
    private bool exhausted = false;
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
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new InternalStateMachine();
        animator = GetComponent<Animator>(); //tl5: added for animation
        sr = GetComponent<SpriteRenderer>();
        noiseMaker = GetComponent<NoiseMaker>();
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleStealth();
        HandleSprinting();
        HandleHealth();
        HandleAnimation();
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
    private void HandleSprinting()
    {
        staminabar.SetStamina(exhaustion);
        if (exhaustion <= maxExhaustion && exhausted)
        {
            exhaustion += exhaustionGain;
        } else if (exhausted == false && exhaustion >= 0)
        {
            exhaustion -= exhaustionLoss;
        }
        if (exhaustion > maxExhaustion) exhaustion = maxExhaustion;
        if (exhaustion < 0) {
            exhaustion = 0;
            exhausted = true;
        };
        if (exhausted)
        {
            stateMachine.stopRunningCommand.Execute();
        }
    }

    public void OnSprint(InputValue value)
    {
        if (exhaustion >= 0 && !exhausted)
        {
            stateMachine.toggleRunningCommand.Execute();
        }
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

    // Wrappers

    public int getExhaustion()
    {
        return exhaustion;
    }

    public bool isExhausted()
    {
        return exhausted;
    }

    public int getHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        if (BCmode == false) health -= damage;
        health -= damage;
        healthBar.SetHealth(health);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        health = 100;
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
