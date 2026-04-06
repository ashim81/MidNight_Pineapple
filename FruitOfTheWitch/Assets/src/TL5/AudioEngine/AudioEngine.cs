using UnityEngine;

public class AudioEngine : MonoBehaviour
{

//--------------Inspector Variables------------------:

    [Header("Audio Settings")]
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float sfxVolume = 1f; // new
        [Range(0f, 1f)] public float musicVolume = 1f; // new
        [Range(0f, 1f)] public float ambienceVolume = 1f; // new


    [Header("Audio Clips")]
        public AudioClip footstepClip;
        public AudioClip glassClip;
        public AudioClip floorTrapClip;
        public AudioClip alarmClip;
        public AudioClip coinClip;
        public AudioClip startGameClip;
        public AudioClip menuMusicClip;
        public AudioClip clickClip;
        public AudioClip hoverClip;
        public AudioClip levelAmbienceClip; 
        public AudioClip PigClip;


    [Header("Audio Sources")]
        public AudioSource sfxSource;
        public AudioSource musicSource;
        public AudioSource ambienceSource;

    void Awake()
    {
        if (!sfxSource) sfxSource = GetComponent<AudioSource>(); // Ensure SFX Source is assigned
        if (!ambienceSource) ambienceSource = GetComponent<AudioSource>(); // Ensure Ambience Source is assigned
        ApplyVolumeSettings(); //03/26/24: Apply volume settings on awake
    }

//--------------Public Methods------------------:
//03/26/26: New methods for volume control and getting final volumes for SFX and Music
        public void ApplyVolumeSettings()
    {
        masterVolume = Mathf.Clamp01(masterVolume);
        sfxVolume = Mathf.Clamp01(sfxVolume);
        musicVolume = Mathf.Clamp01(musicVolume);
        ambienceVolume = Mathf.Clamp01(ambienceVolume);

        if (musicSource != null)
        {
            musicSource.volume = masterVolume * musicVolume;
        }
    }

    public void PlayMenuMusic()
    {
    if (musicSource == null || menuMusicClip == null) return;

    musicSource.clip = menuMusicClip;
    musicSource.volume = GetFinalMusicVolume();
    musicSource.loop = true;
    musicSource.Play();
    }

    public float GetFinalSFXVolume()
    {
        return Mathf.Clamp01(masterVolume) * Mathf.Clamp01(sfxVolume);
    }

    public float GetFinalMusicVolume()
    {
        return Mathf.Clamp01(masterVolume) * Mathf.Clamp01(musicVolume);
    }

    void PlaySFX(AudioClip clip)
    {
        if (!clip || !sfxSource) return;
        sfxSource.PlayOneShot(clip, GetFinalSFXVolume()); //03/26/26: Use GetFinalSFXVolume to apply master and SFX volume together
    }
    
    //Function for all stealth SFX, called by Sound Engine when it receives a signal from Noise Maker
    
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
                
            case "Coin":
                clip = coinClip;
                break;
            
            case "Click":
                clip = clickClip;
                break;
            
            case "Hover":
                clip = hoverClip;
                break;

            case "LevelAmbience":
                clip= levelAmbienceClip;
                break;

            case "Pig":
                clip = PigClip;
                break;

            default:
                clip = alarmClip; //change this later to a default clip
                break;
        }

        if (clip != null)
        {
            PlaySFX(clip); //03/26/26: Calls helper
        }
    }

//3/26/24: New methods for setting volume levels, also apply the master volume to music immediately
        public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp01(value);
        ApplyVolumeSettings();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        ApplyVolumeSettings(); //03/36/26 new
    }
    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);
        ApplyVolumeSettings();
    }

    public void setAmbienceVolume(float value)
    {
        if (ambienceSource != null)
        {
            ambienceSource.volume = Mathf.Clamp01(value) * masterVolume; // Apply master volume to ambience as well
        }
    }
}


