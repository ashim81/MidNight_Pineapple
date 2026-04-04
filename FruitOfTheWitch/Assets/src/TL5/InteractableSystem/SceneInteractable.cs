using System;
using UnityEngine;

public class SceneInteractable : MonoBehaviour
{
    [SerializeField] private InteractableType interactableType;
    [SerializeField] private bool requiresButtonPress = true;
    [SerializeField] private KeyCode interactKey = KeyCode.E; //Interaction key, default is E
    [SerializeField] private string nextSceneName; //For transitional interactables
    [SerializeField] private bool oneTimeUse = true; //anti-spam
    

    private InteractionLogic _logic;
    private bool _hasBeenUsed = false;
    private GameObject _playerInRange;

    private void Awake() //Checks for type and creates correct logic for each
    {
        _logic = CreateLogic();
    }

    private void Update() //Only for button press-type interactables
    {
        if (requiresButtonPress && _playerInRange != null && Input.GetKeyDown(interactKey))
        {
            TryInteract(_playerInRange);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //What happens when player touches object
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (requiresButtonPress)
        {
            _playerInRange = other.gameObject;
        }
        else
        {
            TryInteract(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) //Prevent interaction outside range
    {
        if (_playerInRange == other.gameObject)
        {
            _playerInRange = null;
        }
    }

    private void TryInteract(GameObject player) //Core function.
    {
        if (oneTimeUse && _hasBeenUsed)
        {
            return;
        }

        InteractionContext context = new InteractionContext(player);
        _logic.Interact(context, this); //polymorphic logic to run correct interactable

        if (oneTimeUse)
        {
            _hasBeenUsed = true;
        }
    }

    private InteractionLogic CreateLogic() //Correct logic based on type
    {
        switch (interactableType)
        {
            case InteractableType.Trapdoor:
                return new TrapdoorInteraction();

            case InteractableType.Ladder:
                return new LadderInteraction();

            case InteractableType.Door:
                return new DoorInteraction();

            case InteractableType.Collectible:
                return new CollectibleInteraction();

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public string GetNextSceneName()
    {
        return nextSceneName;
    }

    public void RequestTransition()
{
    // Plug transition code here
    gameObject.SetActive(false); //temporary testing
}
}
