using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(currentHP <= 0)
        {
            IDmg dmg = gameObject.GetComponent<IDmg>();
            dmg.IsDead();
        }
    }
}
