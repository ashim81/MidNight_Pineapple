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
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuCanvas.SetActive(false);
    }
    public void RestartGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1_Alternative");
    }
    public void ExitToMainMenu()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

}