using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool isPlayerClose = false;
    private bool isTalking = false;
    // Update is called once per frame
    void Update()
    {
        if (isPlayerClose)
        {
            transform.Find("interaction_text").gameObject.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E) && !isTalking)
            {
                isTalking = true;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
        }

        if (!isPlayerClose)
        {
            transform.Find("interaction_text").gameObject.SetActive(false);
            isTalking = false;
            FindObjectOfType<DialogueManager>().EndOfDialogue();
        }
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
