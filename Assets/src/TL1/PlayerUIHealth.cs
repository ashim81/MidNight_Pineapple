using UnityEngine;

public class PlayerUIHealth : MonoBehaviour
{
    public Health playerHealth;
    public RectTransform fillRect;

    private float maxWidth;

    void Start()
    {
        maxWidth = fillRect.sizeDelta.x;
    }

    void Update()
    {
        if (playerHealth == null) return;

        float percent = (float)playerHealth.GetCurrentHealth() / playerHealth.maxHealth;

        fillRect.sizeDelta = new Vector2(maxWidth * percent, fillRect.sizeDelta.y);
    }
}