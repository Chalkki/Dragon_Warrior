using System;
using System.Diagnostics.Tracing;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour
{
    public GameObject[] buttonPrefabs;
    public GameObject panelToAttachButtonsTo;
    public void CreateButton()//Creates a button and sets it up
    {
        foreach(GameObject buttonPrefab in buttonPrefabs)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(panelToAttachButtonsTo.transform);//Setting button parent
            //only get the first char of the button name
            //otherwise the button.name would be 0(Clone)
            // or we could use a count variable instead
        }
    }


}
