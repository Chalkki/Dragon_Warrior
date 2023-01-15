using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActive : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Door;
    private void Start()
    {
        
    }
    private void Update()
    {
       if(Boss == null)
        {
            SetActive();
        }
    }
    public void SetActive()
    {
        Door.SetActive(true);
    }

}
