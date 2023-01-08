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
    public Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        cleanDialogue();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialoguePanel.SetActive(true);
        nameText.text = dialogue.name;
        nextLine();
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
            EndOfDialogue();
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
        dialoguePanel.SetActive(false);
    }
}
