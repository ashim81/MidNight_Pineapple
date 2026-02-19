using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{
    [Header("References")]
    public Tilemap doorTilemap;
    public Transform player;
    public DoorAreaTrigger doorArea;   // drag DoorArea here

    [Header("Settings")]
    public float interactRadius = 1.2f;
    public float closeDelay = 5f;

    private bool doorOpen = false;
    private BoundsInt lastDoorBounds;  // whatever you’re using to store tiles
    private TileBase[] savedTiles;

    void Update()
    {
        // your input + open logic here (unchanged)
    }

    public void OpenDoor(BoundsInt doorBounds)
    {
        if (doorOpen) return;

        doorOpen = true;

        lastDoorBounds = doorBounds;
        savedTiles = doorTilemap.GetTilesBlock(doorBounds);

        // remove tiles
        for (int x = doorBounds.xMin; x < doorBounds.xMax; x++)
        for (int y = doorBounds.yMin; y < doorBounds.yMax; y++)
            doorTilemap.SetTile(new Vector3Int(x, y, 0), null);

        StartCoroutine(TryCloseDoorAfterDelay());
    }

    IEnumerator TryCloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);

        // If player is still in doorway, keep waiting until they leave
        while (doorArea != null && doorArea.PlayerInside)
            yield return null;

        // restore tiles
        doorTilemap.SetTilesBlock(lastDoorBounds, savedTiles);

        doorOpen = false;
    }
}
