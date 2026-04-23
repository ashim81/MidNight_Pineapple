using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        FindFirstObjectByType<DialogueSequencing>().StartDialogue(dialogue);
    }
}
