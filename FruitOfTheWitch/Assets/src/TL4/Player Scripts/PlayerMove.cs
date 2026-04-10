using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveSpeed * moveInput;
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Main Menu" || sceneName ==" Combat_test")
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}