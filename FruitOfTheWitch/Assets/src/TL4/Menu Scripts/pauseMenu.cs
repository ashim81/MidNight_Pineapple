using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{    
    [SerializeField]
    private GameObject pauseMenuCanvas;
    public static PauseMenu Instance;
    private string sceneName;
    public bool GameIsPaused = false;

    void Awake()
    {
        pauseMenuCanvas.SetActive(false);
        if(Instance != null && Instance != this)
        {
            Debug.Log("The old menu is destroyed");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("The Menu Persisists");
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    void Update()
    {
        Debug.Log("Game is Active");
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Debug.Log("Game is Paused");
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        Debug.Log("Game is Resumed");
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuCanvas.SetActive(false);
    }
    public void RestartGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void ExitToMainMenu()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}