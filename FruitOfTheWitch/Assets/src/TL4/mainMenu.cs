using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1_WitchHouse");
    }
    public void PlayBCMode()
    {
        SceneManager.LoadScene("Level1_WitchHouse");
        Debug.Log("BC MODE");
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
