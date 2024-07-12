using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleQuest : MonoBehaviour
{
    [SerializeField] private Toggle isComplete;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    string questName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTitle(string title)
    {
        titleText.text = title;
        questName = title;
        if (PlayerPrefs.HasKey(questName))
        {
            if (PlayerPrefs.GetInt(questName) == 1 )
            {
                isComplete.isOn = true;
            }
            
        }
        else
        {
            isComplete.isOn = false;
        }
    }

    public void SetDescription(string description)
    {
        descriptionText.text = description;
    }

    public void SetQuestCompleted()
    {
        isComplete.isOn = true;
        PlayerPrefs.SetInt(questName, 1);
    }
}
