using System;
using System.Collections;
using UnityEngine;

public class FinishObjcet : ObjectData
{
    [SerializeField] Transform soldierFirstPos;
    [SerializeField] NavMeshManager navMeshManager;
    [SerializeField] ZombieSpawnManager zombieSpawnManager;
    

    private void Awake()
    {
        navMeshManager = GetComponent<NavMeshManager>();
        zombieSpawnManager = GetComponent<ZombieSpawnManager>();
    }


    public void InitializeSetUp(int _spawnMaxCount, int _stageLevel)
    {
        navMeshManager.LoadNavMesh();
        zombieSpawnManager.InitializeSetUp(_spawnMaxCount, _stageLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.player.ArriveDestination(soldierFirstPos.position);
            StartCoroutine(zombieSpawnManager.SpawnZombieLoop());
        }
    }
}
