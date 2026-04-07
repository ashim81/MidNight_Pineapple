using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    public virtual void OnInteract(InteractionContext context) //Can be overridden for corresponding NPC behaviors
    {
        if (animator != null)
        {
             Debug.Log("Pig was interacted with");
            //animator.SetTrigger("Angry"); //Assumes an "Angry" trigger is set up in the Animator for the NPC
        }
    }
}