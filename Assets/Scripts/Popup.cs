using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    [SerializeField] PlayerHP playerHP;
    [SerializeField]GameObject spawnPoint;
    Button settingButton;
    Button resetButton;
    Button closeButton;
    void Start()
    {
        VisualElement rootElement = uIDocument.rootVisualElement;

        resetButton = rootElement.Q<Button>("Reset-Button");
        settingButton = rootElement.Q<Button>("Settings-Button");
        closeButton = rootElement.Q<Button>("Close-Button");

        resetButton.clickable.clicked += ResetButton;
        settingButton.clickable.clicked += SettingsButton;
        closeButton.clickable.clicked += CloseButton;

    }


    void Update()
    {
        
    }

    void CloseButton()
    {
        this.gameObject.SetActive(false);
    }

    void ResetButton()
    {
        PlayerPrefs.SetFloat("restartPointX", spawnPoint.transform.position.x);
        PlayerPrefs.SetFloat("restartPointY", spawnPoint.transform.position.y);

        PlayerPrefs.SetInt("playeHP", playerHP.currentHP);

        PlayerPrefs.SetString("restartScene", SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();
    }

    void SettingsButton()
    {

    }

}
