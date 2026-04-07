using UnityEngine;

public enum InteractableType
{
    Trapdoor,
    Ladder,
    Door,
    Collectible
    //Add more interactable types here
}

public class InteractionContext //Context class to hold interaction data
{
    public GameObject Player { get; }

    public InteractionContext(GameObject player)
    {
        Player = player;
    }
}

public abstract class InteractionLogic //Base class for interaction logic
{
    public abstract void Interact(InteractionContext context, SceneInteractable interactable);
}