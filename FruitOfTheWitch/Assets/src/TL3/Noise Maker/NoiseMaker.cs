using UnityEngine;
using System.Collections;

public class NoiseMaker : MonoBehaviour
{
// Inspector Variables:

[Header("Sound Settings")]

    public float radius = 5f;
    public float expansionSpeed = 5f;
    public LayerMask enemyLayer;

    public SoundType soundToEmit = SoundType.Default;
    
    public enum SoundType
    {
        Default,
        Footstep,
        Glass,
        FloorTrap,
        Alarm
    }

[Header("Visual")]

    public GameObject soundVisual;

[Header("Sound Engine")]

    public GameObject AudioEngine;

// Trigger Sound if Player Detected

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EmitSound());
        }
    }

// Sound Expansion

    private IEnumerator EmitSound()
    {
        float currentRadius = 0f;
        GameObject visual = Instantiate(soundVisual, transform.position, Quaternion.identity);
    
    // Send Signal to Audio Engine

        AudioEngine.SendMessage("PlaySFXGame", soundToEmit.ToString(), SendMessageOptions.DontRequireReceiver);

        while (currentRadius < radius)
        {
        // Loop Control

            currentRadius += expansionSpeed * Time.deltaTime;

        // Scale Visual

            visual.transform.localScale = new Vector3(currentRadius * 2f, currentRadius * 2f, 1f);
        
        // Visual Fade Out Effect

            SpriteRenderer soundCircle = visual.GetComponent<SpriteRenderer>();
            Color alpha = soundCircle.color;
            alpha.a = 1f - (currentRadius / radius);
            soundCircle.color = alpha;

        // Detect Enimies Hit by Soundwave

            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, currentRadius, enemyLayer);

            foreach (Collider2D hit in detectedEnemies)
            {
                hit.SendMessage("OnHearSound", transform.position, SendMessageOptions.DontRequireReceiver);
            }
            
        // Waits a Frame
            yield return null;
        }

        Destroy(visual);
    }
}