using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObjcet : ObjectData
{
    [SerializeField] Transform FinishPos;
    [SerializeField] Transform SoldierFirstPos;
    
    public Transform finishPos { get => FinishPos; }
    public Transform soldierFirstPos { get => SoldierFirstPos; }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.player.ArriveDestination();
        }
    }
}
