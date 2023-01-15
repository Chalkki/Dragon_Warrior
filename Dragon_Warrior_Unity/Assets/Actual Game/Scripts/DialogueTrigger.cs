using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueTrigger : MonoBehaviour
{
    public string Charname;
    public Sprite icon;
    //public Dialogue[] dialogueChoice;
    //public Dialogue dialogue;
    private bool isPlayerClose = false;
    private bool isTalking = false;
    // Update is called once per frame
    void Update()
    {
        if (isPlayerClose)
        {
            transform.Find("interaction_text").gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !isTalking)
            {
                isTalking = true;

                GetComponent<ButtonCreator>().CreateButton();
                FindObjectOfType<DialogueManager>().ShowInfo(Charname, icon);
                FindObjectOfType<DialogueManager>().ShowChoices();
            }
        }

        if (!isPlayerClose)
        {
            transform.Find("interaction_text").gameObject.SetActive(false);
            if (isTalking)
            {
                isTalking = false;
                FindObjectOfType<DialogueManager>().EndOfDialogue();
            }
        }
    }

    public void ChoiceMade(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().DisableChoices();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerClose = false;
    }
}
