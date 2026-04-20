using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    private LevelManager levelmanager;
    public void Update(){
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName != "Main Menu")
        {
            mainMenuCanvas.SetActive(false);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Cutscene_intro");
    }
    public void PlayBCMode()
    {
        Debug.Log("BC MODE Selected");
        LevelManager.instance.BCMode(); 
        SceneManager.LoadScene("Cutscene_intro");
    }
        public void LoadSave()
    {
        Debug.Log("Combat_test");
        SceneManager.LoadScene("Combat_test");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
        
}
