using UnityEngine;
using Unity.Cinemachine;

public class SectionTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D targetBounds;
    [SerializeField] private CinemachineConfiner2D confiner;

    private void Awake()
    {
        if (confiner == null)
            confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (confiner == null || targetBounds == null)
            return;

        confiner.BoundingShape2D = targetBounds;
        confiner.InvalidateBoundingShapeCache();
    }
}