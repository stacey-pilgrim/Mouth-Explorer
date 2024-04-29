using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonEvents : MonoBehaviour
{
    private UIDocument document;
    private Button settingsButton;

    public GameObject settingsPanel;
    void Awake()
    {
        document = GetComponent<UIDocument>();
        settingsButton = document.rootVisualElement.Q("SettingsButton") as Button;
        settingsButton.clicked += OnSettingsButtonClick;

        settingsPanel.SetActive(false);
    }

    private void OnSettingsButtonClick()
    {
        Debug.Log("Settings");
        if (settingsPanel.activeInHierarchy == false)
        {
 
            settingsPanel.SetActive(true);
        } 
        else
        {
            settingsPanel.SetActive(false);
        }
    }
}
