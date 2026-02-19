using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private GateDoor currentDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var door = other.GetComponent<GateDoor>();
        if (door != null) currentDoor = door;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var door = other.GetComponent<GateDoor>();
        if (door != null && door == currentDoor) currentDoor = null;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentDoor != null)
                currentDoor.TryOpen();
        }
    }
}
