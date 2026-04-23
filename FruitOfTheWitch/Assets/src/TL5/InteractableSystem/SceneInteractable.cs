using UnityEngine;

public enum InteractableType
{
    Collectible,
    Transition,
    NPC
}

public class SceneInteractable : MonoBehaviour //Scene object
{
    [SerializeField] private InteractableType type;
    [SerializeField] private bool requiresKeyPress = true;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private AudioEngine audioEngine;
    [SerializeField] private string soundName = "Coin";

    private GameObject playerInRange;

    private void Awake()
    {
        if (type == InteractableType.Collectible)
            requiresKeyPress = false;
    }

        private void InteractionStart(GameObject player)
    {
    //    InteractionLogic logic = new CollectibleInteraction();
    //
    //    logic.PlaySound(this);        // static
    //    logic.Interact(player, this); // dynamic
    //
    //    return;
    
        InteractionLogic logic; //Static Type InteractionLogic (Grandpa* pGrandpa)

        switch (type) //Object creation and assignment
        {
            case InteractableType.Collectible:
                logic = new CollectibleInteraction();
                break;
            case InteractableType.Transition:
                logic = new TransitionInteraction();
                break;
            case InteractableType.NPC:
                logic = new NPCInteraction();
                break;
            default:
                logic = new InteractionLogic();
                break;
        }
        
        logic.PlaySound(this); // statically bound (everything will play same sound)
        logic.Interact(player, this);    // dynamically bound (Interact is virtual)
    }

    private void Update()
    {
        if (requiresKeyPress && playerInRange != null && Input.GetKeyDown(interactKey))
        {
            InteractionStart(playerInRange);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (requiresKeyPress)
            playerInRange = other.gameObject;
        else
            InteractionStart(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerInRange == other.gameObject)
            playerInRange = null;
    }

    public void PlaySound()
{
    if (audioEngine != null && !string.IsNullOrEmpty(soundName))
    {
        audioEngine.PlaySFXGame(soundName);
    }
}

    public void OnTransitionTriggered()
    {
        Debug.Log("Transition happens here");
    }

    public void OnNPCTriggered()
    {
        Debug.Log("NPC interaction happens here");
    }
}