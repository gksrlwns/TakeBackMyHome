using System.Collections;
using UnityEngine;

public class FinishObjcet : ObjectData
{
    [SerializeField] Transform soldierFirstPos;
    [SerializeField] NavMeshManager navMeshManager;
    [SerializeField] SpawnArea spawnArea;
    [SerializeField] float spawnTime = 1 ;
    [SerializeField] int spawnMaxCount = 30;
    [SerializeField] int spawnCurCount = 0;
    [SerializeField] int startSpawnCount = 10;
    public int deathCount = 0;

    private void Awake()
    {
        navMeshManager = GetComponent<NavMeshManager>();
        spawnArea = GetComponentInChildren<SpawnArea>();
    }

    public void InitializeSetUp(int _spawnMaxCount, float _spawnTime)
    {
        navMeshManager.LoadNavMesh();
        spawnCurCount = 0;
        deathCount = 0;
        spawnTime = _spawnTime;
        spawnMaxCount = _spawnMaxCount;
        
    }
    void TestSpawnZombie()
    {
        Zombie zombie = PoolManager.instance.GetPool<Zombie>(PoolType.Zombie);
        zombie.transform.position = spawnArea.SpawnPoint();
        spawnCurCount++;
    }

    IEnumerator SpawnZombie()
    {
        for(int i = 0; i < startSpawnCount; i++)
        {
            Zombie zombie = PoolManager.instance.GetPool<Zombie>(PoolType.Zombie);
            zombie.transform.position = spawnArea.SpawnPoint();
            spawnCurCount++;
        }
        while(true)
        {
            if (spawnCurCount >= spawnMaxCount) yield break;
            Zombie zombie = PoolManager.instance.GetPool<Zombie>(PoolType.Zombie);
            zombie.transform.position = spawnArea.SpawnPoint();
            yield return CoroutineManager.DelaySeconds(spawnTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.player.ArriveDestination(soldierFirstPos.position);
            StartCoroutine(SpawnZombie());
            //TestSpawnZombie();
        }
    }
}
