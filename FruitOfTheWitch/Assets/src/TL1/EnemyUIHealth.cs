using UnityEngine;

public class EnemyUIHealth : MonoBehaviour
{
    public Health enemyHealth;
    public RectTransform fillRect;

    private float maxWidth;

    void Start()
    {
        if (fillRect == null)
        {
            Debug.LogError("FillRect is NOT assigned!");
            return;
        }

        maxWidth = fillRect.sizeDelta.x;
    }

    void Update()
    {
        if (enemyHealth == null || fillRect == null) return;

        float percent = (float)enemyHealth.GetCurrentHealth() / enemyHealth.maxHealth;
        fillRect.sizeDelta = new Vector2(maxWidth * percent, fillRect.sizeDelta.y);
    }
}