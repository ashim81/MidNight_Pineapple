using UnityEngine;

public class HUD : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
