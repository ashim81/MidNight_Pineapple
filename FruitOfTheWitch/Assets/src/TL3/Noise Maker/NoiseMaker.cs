using UnityEngine;
using System.Collections;

public class NoiseMaker : DetectorSuperclass
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

// D Y N A M I C  B I N D I N G

private DetectorSuperclass d_NoiseMaker;

    void Start()
    {
        d_NoiseMaker = this;
    }

    public override void PerformDetection()
    {
        Debug.Log("This is the virtual overide of PerformDetection()");
        StartCoroutine(EmitSound());
    }

// Trigger Sound if Player Detected
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            d_NoiseMaker.PerformDetection();
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
        
        //////////////////////// COPYWRITE VIOLATION ///////////////////////////////////////////////////////
        // Code copied directly from: https://discussions.unity.com/t/how-to-change-alpha-of-a-sprite/137534
        // No credit given to user "zeppike"
        // Website visited 03/30/2026 from Brandon Lunney's personal computer.
        // Code only lightly modified to fit the structure of the rest of the code.
        // Fair Use: Is for a school project to demonstrate a copywrite violation.

        // Visual Fade Out Effect
            SpriteRenderer soundCircle = visual.GetComponent<SpriteRenderer>();
            Color alpha = soundCircle.color;
            alpha.a = 1f - (currentRadius / radius);
            soundCircle.color = alpha;
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        
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

//////////////////////// TL2+: Radius Interface ////////////////////////////////
    public void setRadius(float newRadius)
    {
        if (newRadius >= 0)
        {
            radius = newRadius;
        }
    
        else
        {
            radius = 0;
            Debug.LogWarning("Cannot set a negative radius on NoiseMaker.");
        }
    }

    public float getRadius()
    {
        return radius;
    }
///////////////////////////////////////////////////////////////////////////////
}