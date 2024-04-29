using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsEvents : MonoBehaviour
{
    private UIDocument document;
    private Button saveButton;
    private Button resetButton;
    private Button quitButton;

    void Awake()
    {
        document = GetComponent<UIDocument>();

        resetButton = document.rootVisualElement.Q("ResetButton") as Button;
        saveButton = document.rootVisualElement.Q("SaveButton") as Button;
        quitButton = document.rootVisualElement.Q("QuitButton") as Button;

        resetButton.clicked += OnResetButtonClick;
        saveButton.clicked += OnSaveButtonClick;
        quitButton.clicked += OnQuitButtonClick;
    }

    private void OnResetButtonClick()
    {
        Debug.Log("Reset");
    }

    private void OnSaveButtonClick()
    {
        Debug.Log("Save");
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
