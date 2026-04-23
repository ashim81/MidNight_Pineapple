using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private GameObject Player;
    private GameObject playerInstance;
    [SerializeField]
    private bool isBCMode = false;
    public bool IsBCMode => isBCMode;

    //Gamemanager Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BCMode()
    {
        isBCMode = true;
        Debug.Log("BCMODE is active!");
    }
    public void LoadMainMenu()
    {
        isBCMode = false;
        SceneManager.LoadSceneAsync("Main Menu");
    }
    public void EnterHiddenLevel()
    {
        SceneManager.LoadSceneAsync("HiddenLevel");
    }
    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        if (isBCMode)
        {
            Debug.Log("BcMode is false");
        }
        else
        {
            Debug.Log("BCMode is true");
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

}