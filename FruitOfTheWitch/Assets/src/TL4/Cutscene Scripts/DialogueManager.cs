using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private LevelManager levelmanager;
    private Queue<string> sentences = new Queue<string>();
    private DialogueTrigger dialogueTrigger;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0 || sentences.Count == 0)
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

    public virtual void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        LevelManager.instance.LoadScene("Main Menu");
    }

}
