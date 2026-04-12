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
    
    private bool sneaky;
    private int exhaustion;

    
    private bool isExhausted = true;
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
       // staminabar.SetMaxStamina(maxExhaustion);
        noiseMaker = GetComponent<NoiseMaker>();
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
        sneaky = stateMachine.isSneaky();
    }

    public void OnCrouch(InputValue value)
    {
        stateMachine.RunCommand(InternalStateMachine.Command.ToggleSneak);
    }

    // Sprinting
    private void HandleSprinting()
    {
        staminabar.SetStamina(exhaustion);
        if (exhaustion <= maxExhaustion && isExhausted)
        {
            exhaustion += exhaustionGain;
        } else if (isExhausted == false && exhaustion >= 0)
        {
            exhaustion -= exhaustionLoss;
        }
        if (exhaustion > maxExhaustion) exhaustion = maxExhaustion;
        if (exhaustion < 0) {
            exhaustion = 0;
            isExhausted = true;
        };
        if (isExhausted)
        {
            stateMachine.RunCommand(InternalStateMachine.Command.StopRunning);
        }
    }

    public void OnSprint(InputValue value)
    {
        if (exhaustion >= maxExhaustion)
        {
            isExhausted = false;
            stateMachine.RunCommand(InternalStateMachine.Command.ToggleRunning);
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
    public bool isSneaky()
    {
        return sneaky;
    }

    public int getExhaustion()
    {
        return exhaustion;
    }

    public int getHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        if (BCmode == false) health -= damage;
        if (health <= 0) health = 0;
        healthBar.SetHealth(health);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        health = 100;
        exhaustion = 0;
        stateMachine.RunCommand(InternalStateMachine.Command.Reset);
    }
}
