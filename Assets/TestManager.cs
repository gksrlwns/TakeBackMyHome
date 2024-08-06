using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] GameObject finishLinePrefab;
    [SerializeField] float spawnTime = 1;
    [SerializeField] int spawnMaxCount = 50;

    void Start()
    {
        FinishObjcet finishObjcet = Instantiate(finishLinePrefab, transform).GetComponent<FinishObjcet>();
        finishObjcet.SetPosition(2);
        finishObjcet.InitializeSetUp(spawnMaxCount, spawnTime);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            GameManager.instance.player.soldierList[0].GetComponent<SoldierHealth>().SufferDamage(1);
        }
    }

}
