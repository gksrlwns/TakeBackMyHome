using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    [SerializeField] SpawnArea spawnArea;
    [SerializeField] Transform endPoint;
    [SerializeField] float spawnTime = 1;
    [SerializeField] int spawnMaxCount = 30;
    [SerializeField] int spawnCurCount = 0;
    [SerializeField] int startSpawnCount = 10;
    [SerializeField] int stageLevel = 1;
    [SerializeField] float zombieBasicweight = 45f;
    [SerializeField] float zombieRunweight = 10f;
    public int deathCount = 0;

    Dictionary<ZombieType, float> zombieWeightDict;
    float totalWeight = 100;

    private void Awake()
    {
        spawnArea = GetComponentInChildren<SpawnArea>();
    }

    public void InitializeSetUp(int _spawnMaxCount, int _stageLevel)
    {
        spawnCurCount = 0;
        deathCount = 0;
        spawnMaxCount = _spawnMaxCount;
        stageLevel = _stageLevel;
        zombieBasicweight = 0f;
        zombieRunweight = 10f;
        totalWeight = 100;
        zombieRunweight += stageLevel;
        zombieBasicweight = (totalWeight - zombieRunweight)/2;
        zombieWeightDict = new Dictionary<ZombieType, float>()
        {
            {ZombieType.A, zombieBasicweight},
            {ZombieType.B, zombieBasicweight},
            {ZombieType.Running, zombieRunweight},
        };
        
    }
    public IEnumerator SpawnZombieLoop()
    {
        for (int i = 0; i < startSpawnCount; i++) SpawnZombie();

        while (true)
        {
            if (spawnCurCount >= spawnMaxCount) break;
            SpawnZombie();
            yield return CoroutineManager.DelaySeconds(spawnTime);
        }
        
        StartCoroutine(CheckZombieCount());
    }

    void SpawnZombie()
    {
        Zombie zombie = PoolManager.instance.GetPool<Zombie>(RandomZombieSelect());
        zombie.transform.position = spawnArea.SpawnPoint();
        zombie.GetEndPoint(endPoint);
        spawnCurCount++;
    }

    IEnumerator CheckZombieCount()
    {
        while(true)
        {
            if (spawnCurCount <= 0)
            {
                GameManager.instance.CompleteGame();
                yield break;
            }

            yield return CoroutineManager.DelaySeconds(1f);
        }
    }

    PoolType RandomZombieSelect()
    {
        float randomWeight = UnityEngine.Random.Range(0, totalWeight);
        return (randomWeight < zombieWeightDict[ZombieType.A]) ? PoolType.ZombieA : 
            (randomWeight < zombieWeightDict[ZombieType.A] + zombieWeightDict[ZombieType.B]) ? PoolType.ZombieB :
            PoolType.ZombieRun;
    }

}
