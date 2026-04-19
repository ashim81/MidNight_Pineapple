using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;   
public class DialogueSequencing : DialogueManager
{
    public override void EndDialogue()
    {
        animator.SetBool("isOpen", false);

        if (SceneManager.GetActiveScene().name == "Cutscene_intro")
        {
            LevelManager.instance.LoadScene("Level1_Alternative");
        }
        else
        {
            LevelManager.instance.LoadScene("Main Menu");
        }
    }
}
