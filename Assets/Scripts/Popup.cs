using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    [SerializeField] PlayerHP playerHP;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject settings;
    VisualElement visualPanel;
    Button settingButton;
    Button resetButton;
    Button closeButton;
    void Start()
    {
        CreateVisualElement();

    }

    private void CreateVisualElement()
    {
        VisualElement rootElement = uIDocument.rootVisualElement;
        visualPanel = rootElement.Q<VisualElement>("VisualPanel");
        resetButton = visualPanel.Q<Button>("Reset-Button");
        settingButton = visualPanel.Q<Button>("Settings-Button");
        closeButton = visualPanel.Q<Button>("Close-Button");

        resetButton.clickable.clicked += ResetButton;
        settingButton.clickable.clicked += SettingsButton;
        closeButton.clickable.clicked += CloseButton;
    }

    private void OnEnable()
    {
        CreateVisualElement();
    }

    void Update()
    {

    }

    void CloseButton()
    {
        Time.timeScale = 1.0f;
        MenuHandler.IsGamePaused = false;
        Debug.Log("closeButton");
        this.gameObject.SetActive(false);
    }

    void ResetButton()
    {
        Debug.Log("restartButton");
        PlayerPrefs.SetFloat("restartPointX", spawnPoint.transform.position.x);
        PlayerPrefs.SetFloat("restartPointY", spawnPoint.transform.position.y);

        PlayerPrefs.SetInt("playeHP", playerHP.currentHP);

        PlayerPrefs.SetString("restartScene", SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();
    }

    void SettingsButton()
    {
        Debug.Log("settingsButton");
        settings.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
