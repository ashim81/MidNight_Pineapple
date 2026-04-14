using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StartScreenUI : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI selectedText;

    [Header("Main Buttons")]
    public Button startButton;
    public Button mixButton;
    public Button restartButton;

    [Header("Potion Buttons")]
    public Button potion1Button;
    public Button potion2Button;
    public Button potion3Button;
    public Button potion4Button;
    public Button potion5Button;

    private List<Button> selectedButtons = new List<Button>();
    private List<string> selectedPotionNames = new List<string>();

    private List<string> correctPotionNames = new List<string>
    {
        "Moon Drop",
        "Forest Essence",
        "Blood Berry"
    };

    void Start()
    {
        potion1Button.gameObject.SetActive(false);
        potion2Button.gameObject.SetActive(false);
        potion3Button.gameObject.SetActive(false);
        potion4Button.gameObject.SetActive(false);
        potion5Button.gameObject.SetActive(false);

        mixButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        selectedText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        startButton.gameObject.SetActive(false);

        potion1Button.gameObject.SetActive(true);
        potion2Button.gameObject.SetActive(true);
        potion3Button.gameObject.SetActive(true);
        potion4Button.gameObject.SetActive(true);
        potion5Button.gameObject.SetActive(true);

        selectedText.gameObject.SetActive(true);

        StartCoroutine(ShowStartMessages());
    }

    IEnumerator ShowStartMessages()
    {
        titleText.text = "You have 5 chemical to make this potion";
        selectedText.text = "";
        yield return new WaitForSeconds(3f);

        titleText.text = "Select 3 chemical to make potion";
        selectedText.text = "";
    }

    public void SelectPotion(Button clickedButton)
    {
        if (selectedButtons.Contains(clickedButton))
        {
            return;
        }

        if (selectedButtons.Count >= 3)
        {
            return;
        }

        selectedButtons.Add(clickedButton);

        string potionName = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
        selectedPotionNames.Add(potionName);

        clickedButton.image.color = Color.yellow;

        titleText.text = "You have selected:";
        selectedText.text = string.Join(", ", selectedPotionNames);

        if (selectedButtons.Count == 3)
        {
            mixButton.gameObject.SetActive(true);
        }
    }

    public void MixPotions()
    {
        if (selectedPotionNames.Count != 3)
        {
            titleText.text = "Please select 3 chemical first";
            return;
        }

        List<string> selectedSorted = new List<string>(selectedPotionNames);
        List<string> correctSorted = new List<string>(correctPotionNames);

        selectedSorted.Sort();
        correctSorted.Sort();

        bool isCorrect = true;

        for (int i = 0; i < correctSorted.Count; i++)
        {
            if (selectedSorted[i] != correctSorted[i])
            {
                isCorrect = false;
                break;
            }
        }

        potion1Button.gameObject.SetActive(false);
        potion2Button.gameObject.SetActive(false);
        potion3Button.gameObject.SetActive(false);
        potion4Button.gameObject.SetActive(false);
        potion5Button.gameObject.SetActive(false);

        mixButton.gameObject.SetActive(false);

        if (isCorrect)
        {
            titleText.text = "Congratulations you have created BlueMoon potion, You pass";
            selectedText.text = "";
            restartButton.gameObject.SetActive(false);
        }
        else
        {
            titleText.text = "Wrong potion, want to try again?";
            selectedText.text = "";
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        selectedButtons.Clear();
        selectedPotionNames.Clear();

        ResetPotionButton(potion1Button);
        ResetPotionButton(potion2Button);
        ResetPotionButton(potion3Button);
        ResetPotionButton(potion4Button);
        ResetPotionButton(potion5Button);

        restartButton.gameObject.SetActive(false);
        mixButton.gameObject.SetActive(false);

        potion1Button.gameObject.SetActive(true);
        potion2Button.gameObject.SetActive(true);
        potion3Button.gameObject.SetActive(true);
        potion4Button.gameObject.SetActive(true);
        potion5Button.gameObject.SetActive(true);

        titleText.text = "Select 3 chemical to make potion";
        selectedText.text = "";
        selectedText.gameObject.SetActive(true);
    }

    private void ResetPotionButton(Button button)
    {
        button.image.color = new Color(0.8f, 0.2f, 0.2f, 1f);
    }
}