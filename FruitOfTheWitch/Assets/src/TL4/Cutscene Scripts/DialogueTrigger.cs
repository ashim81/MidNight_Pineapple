using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        var manager = FindFirstObjectByType<DialogueManager>();

        if (manager != null)
        {
            manager.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogError("No DialogueManager found in the scene!");
        }
    }
}
