using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(DoorAreaTrigger))]
public class GateDoor : MonoBehaviour
{
    [Header("References")]
    public Tilemap gateTilemap;

    [Header("Settings")]
    public float closeDelay = 5f;

    private DoorAreaTrigger doorArea;
    private BoxCollider2D triggerCol;

    private bool isOpen;
    private Coroutine closeRoutine;

    private readonly List<Vector3Int> doorCells = new List<Vector3Int>();
    private TileBase[] savedTiles;

    private void Awake()
    {
        doorArea = GetComponent<DoorAreaTrigger>();
        triggerCol = GetComponent<BoxCollider2D>();

        triggerCol.isTrigger = true; // important

        BuildCellCacheFromCollider();
    }

    private void BuildCellCacheFromCollider()
    {
        doorCells.Clear();

        if (gateTilemap == null)
        {
            Debug.LogError($"[{name}] GateDoor: gateTilemap is not assigned!");
            return;
        }

        // Convert collider bounds to cell bounds (shrink a tiny bit to avoid grabbing neighbor tiles)
        Bounds b = triggerCol.bounds;
        Vector3 minW = b.min + new Vector3(0.01f, 0.01f, 0);
        Vector3 maxW = b.max - new Vector3(0.01f, 0.01f, 0);

        Vector3Int minC = gateTilemap.WorldToCell(minW);
        Vector3Int maxC = gateTilemap.WorldToCell(maxW);

        // Collect all cells in that rectangle
        for (int x = minC.x; x <= maxC.x; x++)
        {
            for (int y = minC.y; y <= maxC.y; y++)
            {
                doorCells.Add(new Vector3Int(x, y, 0));
            }
        }

        // Save original tiles
        savedTiles = new TileBase[doorCells.Count];
        for (int i = 0; i < doorCells.Count; i++)
        {
            savedTiles[i] = gateTilemap.GetTile(doorCells[i]);
        }
    }

    public void TryOpen()
    {
        if (gateTilemap == null) return;

        // If you moved/resized the trigger in edit mode, rebuild cache
        // (safe to call; if you don't move it, it just re-saves same cells)
        BuildCellCacheFromCollider();

        if (!isOpen)
        {
            isOpen = true;
            ClearDoorTiles();
        }

        if (closeRoutine != null) StopCoroutine(closeRoutine);
        closeRoutine = StartCoroutine(CloseWhenClear());
    }

    private void ClearDoorTiles()
    {
        for (int i = 0; i < doorCells.Count; i++)
            gateTilemap.SetTile(doorCells[i], null);

        gateTilemap.RefreshAllTiles();
    }

    private IEnumerator CloseWhenClear()
    {
        yield return new WaitForSeconds(closeDelay);

        // Wait until player leaves the trigger
        while (doorArea != null && doorArea.PlayerInside)
            yield return null;

        RestoreDoorTiles();
        isOpen = false;
        closeRoutine = null;
    }

    private void RestoreDoorTiles()
    {
        for (int i = 0; i < doorCells.Count; i++)
            gateTilemap.SetTile(doorCells[i], savedTiles[i]);

        gateTilemap.RefreshAllTiles();
    }
}
