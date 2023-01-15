using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResponse : MonoBehaviour
{
    public Dialogue[] dialogueChoice;
    public string ButtonBelongsTo;
    // set index to zero as default

    public void MakeChoice(int index)
    {
        Dialogue dialogue = dialogueChoice[index];
        if(GameObject.Find(ButtonBelongsTo) != null)
        {
            Debug.Log(transform.parent.name);
            GameObject.Find(ButtonBelongsTo).GetComponent<DialogueTrigger>().ChoiceMade(dialogue);
        }
        else
        {
            Debug.Log("The button owner of " + transform.name + " is null");
        }
    }
    public void MakeChoice(string prefix, int index, int sentenceIndex)
    {

        Dialogue dialogue = dialogueChoice[index];
        string oldSentence = dialogue.sentences[sentenceIndex];
        dialogue.sentences[sentenceIndex] = prefix + dialogue.sentences[sentenceIndex];
        if (GameObject.Find(ButtonBelongsTo) != null)
        {
            GameObject.Find(ButtonBelongsTo).GetComponent<DialogueTrigger>().ChoiceMade(dialogue);
        }
        else
        {
            Debug.Log("The button owner of " + transform.name + " is null");
        }
        // because we pass parameter by reference, we need to reset the sentence we changed. 
        dialogue.sentences[sentenceIndex] = oldSentence;
    }

    public void MakeChoice(int index, int sentenceIndex, string suffix)
    {
        Dialogue dialogue = dialogueChoice[index];
        string oldSentence = dialogue.sentences[sentenceIndex];
        dialogue.sentences[sentenceIndex] = dialogue.sentences[sentenceIndex] + suffix;
        if (GameObject.Find(ButtonBelongsTo) != null)
        {
            GameObject.Find(ButtonBelongsTo).GetComponent<DialogueTrigger>().ChoiceMade(dialogue);
        }
        else
        {
            Debug.Log("The button owner of " + transform.name + " is null");
        }
        // because we pass parameter by reference, we need to reset the sentence we changed. 
        dialogue.sentences[sentenceIndex] = oldSentence;
    }
}
