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

    public void SetPowered(bool powered)
    {
        if (!powered)
        {
            slider.fillRect.GetComponent<Image>().color = new Color(153f/255f, 0f, 0f);
        } else
        {
            slider.fillRect.GetComponent<Image>().color = new Color(0f, 0f, 1f);
        }
        
    }
}
