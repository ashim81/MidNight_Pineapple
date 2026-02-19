using UnityEngine;

public class StepSoundTrigger : MonoBehaviour
{
    public bool allowPlayer = true;
    public bool allowNPC = true;

    public AudioClip stepClip;
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource audioSource;
    private readonly System.Collections.Generic.HashSet<Collider2D> inside =
        new System.Collections.Generic.HashSet<Collider2D>();

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CanTrigger(other)) return;

        // play once when entering
        if (inside.Add(other))
        {
            if (stepClip != null)
                audioSource.PlayOneShot(stepClip, volume);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inside.Remove(other);
    }

    private bool CanTrigger(Collider2D other)
    {
        return (allowPlayer && other.CompareTag("Player")) ||
               (allowNPC && other.CompareTag("NPC"));
    }
}
