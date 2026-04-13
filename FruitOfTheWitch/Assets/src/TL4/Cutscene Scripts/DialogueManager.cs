using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private LevelManager levelmanager;
    private Queue<string> sentences;
    private DialogueTrigger dialogueTrigger;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        Debug.Log("Starting Conversations with " + dialogue.name);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Debug.Log("this has been called First ");
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        Debug.Log("this has been called ");
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence; 
        }

    }

    private void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        LevelManager.instance.LoadScene("Level1_Alternative");
    }

}
