using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winText;

    public void ShowWin()
    {
        winText.SetActive(true);
        Time.timeScale = 0f; // freeze game
    }
}