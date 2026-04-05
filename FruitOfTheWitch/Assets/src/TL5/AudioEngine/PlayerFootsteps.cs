using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private AudioEngine audioEngine;

    public void PlayFootstep()
    {
        if (audioEngine != null)
        {
            audioEngine.PlaySFXGame("Footstep");
        }
    }
}