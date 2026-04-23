using UnityEngine;

public class InteractionLogic //Superclass (parent)
{
    //Dynamic Binding 
    //(child classes can replace this method with their version)
    public virtual void Interact(GameObject player, SceneInteractable interactable)
    {
        Debug.Log("Base interaction");
    }

    public void PlaySound(SceneInteractable interactable) //static (plays same sound)
    {
        Debug.Log("Playing sound");
        interactable.PlaySound();
    }
}

//-----------SUBCLASSES----------
public class CollectibleInteraction : InteractionLogic
{
    public override void Interact(GameObject player, SceneInteractable interactable)
    {
        Debug.Log("Coin collected");
        interactable.gameObject.SetActive(false);
    }
}

public class TransitionInteraction : InteractionLogic
{
    public override void Interact(GameObject player, SceneInteractable interactable)
    {
        Debug.Log("Door interaction");
        interactable.OnTransitionTriggered(); //transition hook
    }
}

public class NPCInteraction : InteractionLogic
{
    public override void Interact(GameObject player, SceneInteractable interactable)
    {
        Debug.Log("NPC interaction");
        interactable.OnNPCTriggered(); //behaviorNPC hook
    }
}