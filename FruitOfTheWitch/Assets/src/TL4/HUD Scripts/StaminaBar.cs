using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }
    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }

    public void SetPowered(bool powered)
    {
        if (powered)
        {
            slider.fillRect.GetComponent<Image>().color = new Color(0f, 1f, 1f);
        } else
        {
            slider.fillRect.GetComponent<Image>().color = new Color(0f, 190f/255f, 1f);
        }
        
    }
}
