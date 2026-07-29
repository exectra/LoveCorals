using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject profilePanel;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenProfile()
    {
        profilePanel.SetActive(true);
    }

    public void CloseProfile()
    {
        profilePanel.SetActive(false);
    }


}
