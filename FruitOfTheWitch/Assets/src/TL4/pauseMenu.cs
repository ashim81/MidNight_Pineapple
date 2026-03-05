using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class pauseMenu : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            openPauseMenu();
        }
    }

    private void openPauseMenu()
    {
        Debug.Log("Pause Menu Opened");
    }
 /*   public void ReturntoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
*/
}
