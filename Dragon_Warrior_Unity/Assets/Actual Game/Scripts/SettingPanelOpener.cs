using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelOpener : MonoBehaviour
{
    //private Transform background;
    //private Transform level;
    //private Transform restart;
    //private Transform resume;


    //private void Awake()
    //{
    //    background = transform.Find("background");
    //    level = transform.Find("Level");
    //    restart = transform.Find("Restart");
    //    resume = transform.Find("Resume");
    //}

    public GameObject SettingPanel;

    public void OpenSetting()
    {
        SettingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        SettingPanel.SetActive(false);
    }

    public void PanelSwitch()
    {
        if (SettingPanel.activeSelf)
        {
            CloseSetting();
        }
        else
        {
            OpenSetting();
        }
    }
}
