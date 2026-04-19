using UnityEngine;
using System.Collections;

public class NoiseMaker : MonoBehaviour
{
[Header("Sound Settings")]

    [SerializeField] private float radius = 5f;
    [SerializeField] private float expansionSpeed = 5f;
    [SerializeField] private LayerMask enemyLayer;

//////////////////////// TL5: Audio Engine Interface /////////////////////
    [SerializeField] private SoundType soundToEmit = SoundType.Default;
    private GameObject audioEngine;
    
    public enum SoundType
    {
        Default,
        Footstep,
        Glass,
        FloorTrap,
        Alarm,
        PiggyBoi
        // Nastia Add Your Sounds Here
    }
//////////////////////////////////////////////////////////////////////////

[Header("Visual")]

    [SerializeField] private GameObject soundVisual;

//////////////////////// TL5: Audio Engine Interface /////////////////////
    private void Awake()
    {
        // Find AudioEngine in Scene
        if (audioEngine == null)
        {
            audioEngine = GameObject.Find("AudioEngine");
        }

        // Debug for funzies
        if (audioEngine == null)
        {
            Debug.LogWarning($"No AudioEngine in Scene Hierarchy.");
        }
    }
//////////////////////////////////////////////////////////////////////////

// Trigger Sound if Player Detected
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EmitSound());
        }
    }

// Sound Expansion Visual
    private IEnumerator EmitSound()
    {
        float currentRadius = 0f;
        GameObject visual = Instantiate(soundVisual, transform.position, Quaternion.identity);

        // To Fix Render Order
        SpriteRenderer visualRenderer = visual.GetComponent<SpriteRenderer>();
        if (visualRenderer != null)
        {
            visualRenderer.sortingLayerName = "Foreground";
        }
    
        //////////////////////// TL5: Audio Engine Interface /////////////////////

        audioEngine.SendMessage("PlaySFXGame", soundToEmit.ToString(), SendMessageOptions.DontRequireReceiver);
        
        //////////////////////////////////////////////////////////////////////////
        
        while (currentRadius < radius)
        {
            currentRadius += expansionSpeed * Time.deltaTime;

        // Scale Visual
            visual.transform.localScale = new Vector3(currentRadius * 2f, currentRadius * 2f, 1f);
        
        // Visual Fade Out Effect
            SpriteRenderer soundCircle = visual.GetComponent<SpriteRenderer>();
            Color alpha = soundCircle.color;
            alpha.a = 1f - (currentRadius / radius);
            soundCircle.color = alpha;

        // Detect Enemies Hit by Soundwave
            Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(transform.position, currentRadius, enemyLayer);

            foreach (Collider2D hit in detectedEnemies)
            {
                hit.SendMessage("OnHearSound", transform.position, SendMessageOptions.RequireReceiver);
            }
            
        // Waits a Frame
            yield return null;
        }

        Destroy(visual);
    }

    // TL2+ Added radius.
    public void setRadius(float newRadius)
    {
        radius = newRadius;
    }

    public float getRadius()
    {
        return radius;
    }
}