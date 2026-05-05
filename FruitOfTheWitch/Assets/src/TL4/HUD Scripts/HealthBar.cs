using PlasticGui.WorkspaceWindow.QueryViews.Branches;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetBlue()
    {
        slider.fillRect.GetComponent<Image>().color = Color.blue;
    }
}
