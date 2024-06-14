using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damageValue = 0;
    public int DamageValue { get{ return damageValue; } }
}
