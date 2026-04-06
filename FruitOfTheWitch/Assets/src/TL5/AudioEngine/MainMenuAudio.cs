using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    [SerializeField] private AudioEngine audioEngine;

    void Start()
    {
        if (audioEngine != null)
        {
            audioEngine.PlayMenuMusic();
        }
    }
}