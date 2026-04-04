//Polymorphic interaction logic for different types of interactable objects


/*--------------Transitional Interactables--------------*/
public class TrapdoorInteraction : InteractionLogic
{
    public override void Interact(InteractionContext context, SceneInteractable interactable)
    {
        interactable.RequestTransition();
    }
}

public class LadderInteraction : InteractionLogic
{
    public override void Interact(InteractionContext context, SceneInteractable interactable)
    {
        interactable.RequestTransition();
    }
}

public class DoorInteraction : InteractionLogic
{
    public override void Interact(InteractionContext context, SceneInteractable interactable)
    {        
        interactable.RequestTransition();
    }
}

/*--------------Collectible Interactables--------------*/
public class CollectibleInteraction : InteractionLogic
{
    public override void Interact(InteractionContext context, SceneInteractable interactable)
    {
        interactable.gameObject.SetActive(false);
    }
}