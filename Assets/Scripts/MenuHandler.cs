using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    Button pauseButton;
    Toggle muteToggle;


    void Start()
    {
        VisualElement rootElement = uIDocument.rootVisualElement;

        pauseButton = rootElement.Q<Button>("pause-button");
        muteToggle = rootElement.Q<Toggle>("mute-toggle");

        pauseButton.clickable.clicked += pauseButton_Clicked;
        //Funkce do mute jak zrobi siê zwiêki w grze
    }

    private void pauseButton_Clicked()
    {
        Debug.Log("Pause clicked.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
