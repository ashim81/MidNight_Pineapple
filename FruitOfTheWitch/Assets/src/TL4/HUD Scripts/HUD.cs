using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    //[SerializeField]    
    private PlayerController playercontroller;
    public GameObject sneakEffect;
    private bool sneak;
    private void Awake()
    {
        playercontroller = FindFirstObjectByType<PlayerController>();
    }
    private void Update()
    {
        SneakingEffect();
    }
    private void SneakingEffect()
    {
        sneak = playercontroller.isSneaky();
        if(sneak)
        {
            sneakEffect.SetActive(true);
        }
        else
        {
            sneakEffect.SetActive(false);
        }
    }
}
