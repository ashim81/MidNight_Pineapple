using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winMessage;

    public void ShowWin()
    {
        winMessage.SetActive(true);
    }
}