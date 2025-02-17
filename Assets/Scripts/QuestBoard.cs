using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class QuestBoard : MonoBehaviour
{
    [SerializeField] private GameObject contentGO;
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private GameObject questPanel;
    [SerializeField] GameObject player;
    [SerializeField] private PlayerStatistics playerStatistics;
    int dmgCount = 0;
    int questWellIndexScene = 1;
    PlayerMovement playerMovement;  

    public static event Action OnDamageDealt;
    public QuestList[] questList;

    void Awake()
    {
        for (int i = 1; i < questList.Length; i++)
        {
            Instantiate(panelPrefab, contentGO.transform);
        }
        for (int i = 0; i < contentGO.transform.childCount; i++)
        {
            SingleQuest singleQuest = contentGO.transform.GetChild(i).GetComponent<SingleQuest>();
            singleQuest.SetTitle(questList[i].questName);
            singleQuest.SetDescription(questList[i].questDescription);
        }

        GameObject player = GameObject.Find("Player"); // Znajd� obiekt player (zmie� nazw� "Player" na odpowiedni�)

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();

            

        }

        OnDamageDealt?.Invoke();
    }


    void Update()
    {
        FindMissingGo();
    }

    void FindMissingGo()
    {
        if(questPanel == null)
        {
            GameObject questPP = GameObject.FindGameObjectWithTag("QuestPanel");
            QuestPanelParent questPanelParent = questPP.GetComponent<QuestPanelParent>();
            questPanel = questPanelParent.QuestPanel;
            contentGO = questPanelParent.ContentGO;
            panelPrefab = questPanelParent.PanelPrefab;
            
        }

        if (playerStatistics == null)
        {

            playerStatistics = GameManager.instance.GetPlayerStatistics;
        }

    }
    // buttons

    public void ClosePanel()
    {
        questPanel.SetActive(false);
    }

    public void StinkyTime()
    {
        int qIndex = 0;
        FindMissingGo();
        SingleQuest singleQuest = contentGO.transform.GetChild(qIndex).GetComponent<SingleQuest>();
            singleQuest.SetQuestCompleted();
    }

    public void FirstKillQuest()
    {
        int qIndex = 1;
        FindMissingGo();
        SingleQuest singleQuest = contentGO.transform.GetChild(qIndex).GetComponent<SingleQuest>();
        if (playerStatistics.skeletonsKilled >= 1)
        {
            singleQuest.SetQuestCompleted();
        }
    }

    public void TrueWarriorQuest()
    {
        int qIndex = 2;
        FindMissingGo();
        SingleQuest singleQuest = contentGO.transform.GetChild(qIndex).GetComponent<SingleQuest>();
        if (playerStatistics.skeletonsKilled >= 5)
        {
            singleQuest.SetQuestCompleted();
        }
    }

    public void BloodDonationQuest()
    {
        if (playerMovement != null)
        {
            dmgCount = playerMovement.damageDealt;
        }
        int qIndex = 3;
        FindMissingGo();
        SingleQuest singleQuest = contentGO.transform.GetChild(qIndex).GetComponent<SingleQuest>();
        if (dmgCount >= 500)
        {
            singleQuest.SetQuestCompleted() ;
        }
    }
      
}


    [Serializable]
    public struct QuestList
    {
        public bool isComplete;
        public string questName;
        public string questDescription;
    }