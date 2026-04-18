using UnityEngine;

public class Damageable : MonoBehaviour
{
    public virtual void TakeDamage(int damage)
    {
        Debug.Log("Object took damage");
    }
}