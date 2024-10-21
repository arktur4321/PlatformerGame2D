using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{

    bool isGameRestarted = false;

    void Start()
    {
    }


    void Update()
    {
        if (isGameRestarted)
        {
            isGameRestarted = false;
        this.gameObject.SetActive(false);
        }   
    }

    public void RestartButton()
    {
        PlayerPrefs.DeleteAll();
        isGameRestarted = true;
        SceneManager.LoadScene(1);
        

    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
