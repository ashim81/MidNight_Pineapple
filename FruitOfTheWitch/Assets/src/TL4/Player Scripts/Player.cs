using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            TakeDamage(10);
        }
        if (Keyboard.current.fKey.isPressed)
        {
            GiveHealth(10);
        }
    }

    void GiveHealth(int hp)
    {
        currentHealth += hp;
        healthBar.SetHealth(currentHealth);
        if(currentHealth > 100)
        {
            currentHealth = 100;
        }

    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
