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
    Button StettingButton;
    Button resetButton;
    Button closeButton;
    void Start()
    {
        
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
