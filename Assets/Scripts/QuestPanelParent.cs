using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanelParent : MonoBehaviour
{
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject contentGO;
    [SerializeField] GameObject panelPrefab;
    public GameObject QuestPanel { get { return questPanel; } }
    public GameObject ContentGO { get { return contentGO; } }
    public GameObject PanelPrefab { get { return panelPrefab; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TurnOffQuestPanel()
    {
        questPanel.SetActive(false);
    }
}
