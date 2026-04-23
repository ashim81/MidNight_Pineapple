using UnityEngine;

public class HiddenLevelEntry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hidden Level Entered");
            LevelManager.instance.EnterHiddenLevel();
        }
    }
}
