using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    bool isGamePaused = false;
    Button pauseButton;
    Toggle muteToggle;
    public bool IsGamePaused { get { return isGamePaused; } }

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
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
