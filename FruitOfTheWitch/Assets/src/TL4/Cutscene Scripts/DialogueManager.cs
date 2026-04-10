using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private LevelManager levelmanager;
    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting Conversations with " + dialogue.name);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
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
        LevelManager.instance.LoadScene("Level1_Alternative");
    }

}
