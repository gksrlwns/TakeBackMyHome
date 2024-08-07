using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Player player;
    public bool isPause;
    [SerializeField] int startingSoldierCount = 5;
    [SerializeField] GameObject finishLinePrefab;
    [SerializeField] float spawnTime = 1;
    [SerializeField] int spawnMaxCount = 50;
    [SerializeField] SpawnArea spawnArea;
    //[SerializeField] FinishObjcet finishObjcet;


    //private void OnValidate()
    //{
    //    if (player != null) return;
    //    player = FindObjectOfType<Player>();
    //}

    void Start()
    {
        //var zom = PoolManager.instance.GetPool<Zombie>(PoolType.Zombie);
        //zom.transform.position = spawnArea.SpawnPoint();
        player.CreateSoldier(startingSoldierCount);
        FinishObjcet finishObjcet = Instantiate(finishLinePrefab, transform).GetComponent<FinishObjcet>();
        finishObjcet.InitializeSetUp(spawnMaxCount, spawnTime);
        finishObjcet.SetPosition(2);

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            GameManager.instance.player.soldierList[0].GetComponent<SoldierHealth>().SufferDamage(1);
        }
    }

}
