using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public static PauseMenu Instance{get; private set;}
    public static bool GameIsPaused = false;

    void Awake()
    {
        pauseMenuCanvas.SetActive(false);
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Game is Paused!");
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuCanvas.SetActive(true);
        Debug.Log("Gameis Puase!");
    }
    public void ResumeGame()
    {
                Debug.Log("Gameis resumed!");
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuCanvas.SetActive(false);
                Debug.Log("Gameis Resumed!");
    }
    public void RestartGame()
    {
                Debug.Log("Gameis restarted!");
        SceneManager.LoadScene("Level1_Alternative");
        Debug.Log("Gameis restarted!");
    }
    public void ExitToMainMenu()
    {
        pauseMenuCanvas.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }


}