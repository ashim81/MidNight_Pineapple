using UnityEngine;
using UnityEngine.EventSystems;

public class UIHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioEngine audioEngine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioEngine != null)
        {
            audioEngine.PlaySFXGame("Hover");
        }
    }
}