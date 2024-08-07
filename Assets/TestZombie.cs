using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZombie : MonoBehaviour
{
    SoldierHealth soldierHealth;
    private void Awake()
    {
        soldierHealth = FindAnyObjectByType<SoldierHealth>();
    }
    void Attack()
    {
        Debug.Log("damage");
        soldierHealth.SufferDamage(10);
    }
}
