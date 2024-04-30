using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsEvents : MonoBehaviour
{
    private UIDocument document;
    private Button resetButton, saveButton, loadButton, quitButton;

    void Awake()
    {
        document = GetComponent<UIDocument>();

        resetButton = document.rootVisualElement.Q("ResetButton") as Button;
        saveButton = document.rootVisualElement.Q("SaveButton") as Button;
        loadButton = document.rootVisualElement.Q("LoadButton") as Button;
        quitButton = document.rootVisualElement.Q("QuitButton") as Button;
    }

    private void OnEnable()
    {
        resetButton.clicked += OnResetButtonClick;
        saveButton.clicked += OnSaveButtonClick;
        loadButton.clicked += OnLoadButtonClick;
        quitButton.clicked += OnQuitButtonClick;
    }

    private void OnDisable()
    {
        resetButton.clicked -= OnResetButtonClick;
        saveButton.clicked -= OnSaveButtonClick;
        loadButton.clicked -= OnLoadButtonClick;
        quitButton.clicked -= OnQuitButtonClick;
    }

    private void OnResetButtonClick()
    {
        Debug.Log("Reset");
        DataManager.instance.ResetGame();
    }

    private void OnSaveButtonClick()
    {
        Debug.Log("Save");
        DataManager.instance.SaveGame();
    }

    private void OnLoadButtonClick()
    {
        Debug.Log("Load");
        DataManager.instance.LoadGame();
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
