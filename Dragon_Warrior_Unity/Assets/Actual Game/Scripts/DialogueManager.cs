using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject ChoicePanel;
    public GameObject ContinueButton;
    public Queue<string> sentences;
    public Image Icon;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        ContinueButton.SetActive(true);
        cleanDialogue();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialoguePanel.SetActive(true);

        nextLine();
    }

    public void ShowInfo(string name, Sprite icon)
    {
        nameText.text = name;
        this.Icon.sprite = icon;
    }
    public void ShowChoices()
    {

        dialoguePanel.SetActive(true);
        ChoicePanel.SetActive(true);
        ContinueButton.SetActive(false);
    }
    public void DisableChoices()
    {
        // Destory all the childeren(choice buttons) and disable the choice panel
        ChoicePanel.SetActive(false);
    }
    void cleanDialogue()
    {
        dialogueText.text = "";
        sentences.Clear();
    }


    public void nextLine()
    {
        if (sentences.Count == 0)
        {
            // if the conversation is done, return back to the choice interface
            cleanDialogue();
            ShowChoices();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(Speaking(sentence));
    }
    IEnumerator Speaking(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndOfDialogue()
    {
        cleanDialogue();
        foreach (Transform button in ChoicePanel.transform)
        {
            Destroy(button.gameObject);
        }
        DisableChoices();
        dialoguePanel.SetActive(false);
    }
}
