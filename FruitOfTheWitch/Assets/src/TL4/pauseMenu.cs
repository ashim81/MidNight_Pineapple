using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(true);
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Pause Menu Opened");
            SceneManager.LoadScene("Main Menu");
        }
    }
}