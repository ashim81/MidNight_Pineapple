using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D light2D;

    [Header("Flicker Settings")]
    [SerializeField] private float baseIntensity = 1.2f;
    [SerializeField] private float flickerSpeed = 3f;
    [SerializeField, Range(0f, 0.3f)] private float intensityVariation = 0.15f;

    float seed;

    void Awake()
    {
        if (!light2D)
            light2D = GetComponent<Light2D>();

        seed = Random.value * 1000f;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed + seed, 0f);
        light2D.intensity = baseIntensity + noise * intensityVariation;
    }
}
