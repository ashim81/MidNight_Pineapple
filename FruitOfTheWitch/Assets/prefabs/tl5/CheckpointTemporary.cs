using UnityEngine;
//This script is temporary only to show functional scenes at the moment

public class CheckpointLoadScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            LevelManager.instance.LoadScene(sceneToLoad);
        }
    }
}