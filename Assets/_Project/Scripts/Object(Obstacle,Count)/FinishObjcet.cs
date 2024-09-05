using System;
using System.Collections;
using UnityEngine;

public class FinishObjcetController : ObjectDataController
{
    [SerializeField] Transform soldierFirstPos;
    [SerializeField] NavMeshManager navMeshManager;
    [SerializeField] ZombieSpawnManager zombieSpawnManager;
    [SerializeField] CameraController cameraController;
    

    private void Awake()
    {
        navMeshManager = GetComponent<NavMeshManager>();
        zombieSpawnManager = GetComponent<ZombieSpawnManager>();
        cameraController = GetComponent<CameraController>();
    }


    public void InitializeSetUp(int _spawnMaxCount, int _stageLevel)
    {
        navMeshManager.LoadNavMesh();
        cameraController.InitializeCamera();
        zombieSpawnManager.InitializeSetUp(_spawnMaxCount, _stageLevel);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(SFX.ZombieSpawn);
            GameManager.instance.player.ArriveDestination(soldierFirstPos.position);
            StartCoroutine(zombieSpawnManager.SpawnZombieLoop());
            cameraController.SetCamera();
        }
    }
}
