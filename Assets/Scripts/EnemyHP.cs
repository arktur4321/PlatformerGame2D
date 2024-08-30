using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;



public class EnemyHP : MonoBehaviour
{
    [SerializeField] int maxHP;
    int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {

    }

    public void ReciveDmg(int dmgValue)
    {
        currentHP -= dmgValue;
        if (currentHP <= 0)
        {
            IDmg dmg = gameObject.GetComponent<IDmg>();
            dmg.IsDead();
            Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    { "killCount", 0 },
                };
            AnalyticsService.Instance.CustomData("killed_skeleton", data);

        }
    }
}
