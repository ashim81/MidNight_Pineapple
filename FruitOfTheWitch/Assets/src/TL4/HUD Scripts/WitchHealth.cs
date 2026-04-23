using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WitchHealth : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private bool hasTriggered = false;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        if (health <= 0 && !hasTriggered)
        {
            hasTriggered = true;
            SceneManager.LoadScene("Cutscene_Final");
        }
    }
}