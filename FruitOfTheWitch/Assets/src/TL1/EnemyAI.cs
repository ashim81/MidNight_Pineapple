// using UnityEngine;
// using System.Collections;

// public class EnemyAI : MonoBehaviour
// {
//     public GameObject fireSpellPrefab;
//     public Transform player;
//     public Transform firePoint;

//     public float moveSpeed = 3f;
//     public float attackRange = 1.5f;

//     public float jumpForce = 7f;
//     public float jumpCooldown = 2f;

//     private float lastJumpTime;

//     private Rigidbody2D rb;
//     private EnemyAttack enemyAttack;
//     private Animator animator;
    
//     private int health = 100;
//     [SerializeField]
//     private WitchHealth witchhealth;

//     private bool isGrounded;

//     void Awake()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         enemyAttack = GetComponent<EnemyAttack>();
//         animator = GetComponent<Animator>();
//     }

//     void Start()
//     {
//         StartCoroutine(ShootLoop());
//     }

//     // 🔥 FIRE EVERY 1 SECOND
//     IEnumerator ShootLoop()
//     {
//         while (true)
//         {
//             yield return new WaitForSeconds(1f);
//             ShootFireSpell();
//         }
//     }

//     void FixedUpdate()
//     {
//         if (player == null) return;

//         float distanceX = player.position.x - transform.position.x;

//         // Movement
//         if (Mathf.Abs(distanceX) > attackRange)
//         {
//             float direction = Mathf.Sign(distanceX);
//             rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
//         }
//         else
//         {
//             rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

//             if (enemyAttack != null)
//             {
//                 enemyAttack.TryAttack(player);
//             }
//         }

//         // Jump logic
//         if (Time.time >= lastJumpTime + jumpCooldown)
//         {
//             if (Random.value < 0.3f && isGrounded)
//             {
//                 rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
//                 lastJumpTime = Time.time;
//             }
//         }

//         UpdateAnimation();
//     }

//     void ShootFireSpell()
//     {
//         if (fireSpellPrefab == null || firePoint == null || player == null)
//             return;

//         GameObject spell = Instantiate(fireSpellPrefab, firePoint.position, Quaternion.identity);

//         // ✅ USE BASE CLASS (dynamic binding)
//         Projectile proj = spell.GetComponent<Projectile>();

//         if (proj != null)
//         {
//             proj.SetDirection(player.position);
//         }
//     }

//     void UpdateAnimation()
//     {
//         float velocityX = rb.linearVelocity.x;

//         animator.SetFloat("Speed", Mathf.Abs(velocityX));
//         animator.SetBool("isJumping", !isGrounded);

//         if (velocityX > 0.1f)
//         {
//             animator.Play("Walk_right");
//         }
//         else if (velocityX < -0.1f)
//         {
//             animator.Play("Walk_left");
//         }
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = true;
//         }
//     }

//     void OnCollisionExit2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Ground"))
//         {
//             isGrounded = false;
//         }
//     }

// }