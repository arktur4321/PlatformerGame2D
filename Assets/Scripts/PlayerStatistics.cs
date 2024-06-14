using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public int skeletonsKilled = 0;
    public int distanceTravelled = 0;
    [SerializeField] GameObject player;
    [SerializeField] QuestBoard questBoard;

    void Start()
    {
        Skeleton.OnSkeletonDead += AddKills;
        PlayerInteraction.OnPlayerTravel += PlayerTravel;
        
    }
    private void AddKills()
    {
        skeletonsKilled++;
        questBoard.FirstKillQuest();
        questBoard.TrueWarriorQuest();
        questBoard.BloodDonationQuest();
    }

    private void PlayerTravel()
    {
        questBoard.StinkyTime();
    }


    void Update()
    {
        
    }
}
