using UnityEngine;

public class AudioEngine : MonoBehaviour
{

//--------------Inspector Variables------------------:

    [Header("Audio Settings")]
        [Range(0f, 1f)] public float masterVolume = 1f;


    [Header("Audio Clips")]
        public AudioClip footstepClip;
        public AudioClip glassClip;
        public AudioClip floorTrapClip;
        public AudioClip alarmClip;

        public AudioClip startGameClip;


    [Header("Audio Sources")]
        public AudioSource sfxSource;
        public AudioSource musicSource;

    void Awake()
    {
        if (!sfxSource) sfxSource = GetComponent<AudioSource>(); // Ensure SFX Source is assigned
    }

    /*Before New Implementation
    public void Alarm(Vector3 position) //Plays alarm sound at its position
    {
        PlaySFX(alarmClip);
    }

    public void PlayStartGame() //Plays start game sound
    {
    sfxSource.PlayOneShot(startGameClip, masterVolume);
    } */

    void PlaySFX(AudioClip clip)
    {
        if (!clip || !sfxSource) return;
        sfxSource.PlayOneShot(clip, masterVolume);
    } 

    
    //New Implementation: Function for all stealth SFX, called by Sound Engine when it receives a signal from Noise Maker
    
    public void PlaySFXGame(string soundName)
    {
        if (!sfxSource) return;

        AudioClip clip = null;

        switch (soundName)
        {
            case "Footstep":
                clip = footstepClip;
                break;

            case "Glass":
                clip = glassClip;
                break;

            case "FloorTrap":
                clip = floorTrapClip;
                break;

            case "Alarm":
                clip = alarmClip;
                break;

            default:
                clip = alarmClip; //change this later to a default clip
                break;
        }

        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
