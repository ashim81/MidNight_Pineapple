using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTester : MonoBehaviour
{
    public Weapon weapon;
    public float attackCooldown = 0.5f;

    private float lastAttackTime;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            Time.time >= lastAttackTime + attackCooldown)
        {
            weapon.Activate();
            lastAttackTime = Time.time;
        }
    }
}