using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject InteractionButton;
    [SerializeField] string interactionText;
    [SerializeField] TMP_Text infoText;
    [SerializeField] int currentNumber;
    public static event Action OnPlayerTravel;
    int interactionNumber = -1;
    

    void Start()
    {
        InteractionButton.SetActive(false);
        GameManager.instance.interactionButtonGO = InteractionButton;
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            if(InteractionButton == null)
            {
                InteractionButton = GameObject.FindGameObjectWithTag("InteractionButton").transform.GetChild(0).gameObject;
            }
            InteractionButton.SetActive(true);
            infoText.text = interactionText;
            interactionNumber = currentNumber;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (InteractionButton == null)
            {
                InteractionButton = GameObject.FindGameObjectWithTag("InteractionButton").transform.GetChild(0).gameObject;
            }
            InteractionButton.SetActive(false);
            interactionNumber = -1;
        }
    }

    public void WellEnter()
    {
        switch (interactionNumber)
        {
            case 1 :
                OnPlayerTravel?.Invoke();
            SceneManager.LoadScene(2);
                break;

            case 2 :
                GameManager.instance.OpenQuestPanel();
                Debug.Log("case2");
                break;

        }
    }
}
