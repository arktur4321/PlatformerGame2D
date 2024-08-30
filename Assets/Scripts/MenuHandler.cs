using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    [SerializeField] GameObject popup;
    bool isGamePaused = false;
    Button pauseButton;
    Toggle muteToggle;
    public static bool IsGamePaused = false;

    void Start()
    {
        VisualElement rootElement = uIDocument.rootVisualElement;

        pauseButton = rootElement.Q<Button>("pause-button");
        muteToggle = rootElement.Q<Toggle>("mute-toggle");

        pauseButton.clickable.clicked += pauseButton_Clicked;
        muteToggle.RegisterValueChangedCallback(MuteToogle_Changed);
    }


    private void MuteToogle_Changed(ChangeEvent<bool> evt)
    {
        Debug.Log("mute");
        if (evt.newValue)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
    }

    private void pauseButton_Clicked()
    {
        Debug.Log("Pause clicked.");
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        popup.SetActive(IsGamePaused);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
