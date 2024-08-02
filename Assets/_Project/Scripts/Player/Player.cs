using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Soldiers Info")]
    public int soldierCount;
    public List<Soldier> soldierList;
    [SerializeField] Transform soldierTr;

    [Header("Finish Info")]
    public FinishObjcet finishObjcet;

    [Header("Player Info")]
    PlayerController playerController;
    TargetSearch targetSearch;

    [SerializeField] Bounds spawnPointBounds;
    public Vector3 targetPos;
    public Vector3 soldierFirstPos;

    private void Awake()
    {
        soldierList = new List<Soldier>();
        playerController = GetComponent<PlayerController>();
        targetSearch = GetComponent<TargetSearch>();
    }

    private void Start()
    {
        targetSearch.enabled = false;
    }

    #region Sort Soldier
    public void ArriveDestination()
    {
        playerController.isArrive = true;
        targetSearch.enabled = true;
        Vector3 soldierFirstPos = finishObjcet.soldierFirstPos.position;
        for (int i = 0;  i < soldierList.Count; i++)
        {
            //10칸을 맞추기 위함
            Vector3 soldierPos = new Vector3(soldierFirstPos.x + (i % 10), soldierFirstPos.y, soldierFirstPos.z - (i / 10));
            
            Debug.Log(soldierPos);
            
            soldierList[i].MoveDestination(soldierPos);
            soldierList[i].GetTargetSearch(targetSearch);
        }
    }
    #endregion

    #region Spawn & Create Soldier
    public Vector3 SpawnPoint()
    {
        float randomPointX = transform.position.x + spawnPointBounds.center.x + UnityEngine.Random.Range(spawnPointBounds.extents.x * -0.5f, spawnPointBounds.extents.x * 0.5f);
        float randomPointZ = transform.position.z + spawnPointBounds.center.z + UnityEngine.Random.Range(spawnPointBounds.extents.z * -0.5f, spawnPointBounds.extents.z * 0.5f);

        Vector3 spawnPos = new Vector3(randomPointX, 0.6f, randomPointZ);
        return spawnPos;
    }

    public void CreateSoldier(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Soldier soldier = PoolManager.instance.GetPool<Soldier>(PoolType.Soldier, true);
            
            soldier.transform.parent = soldierTr;
            soldierList.Add(soldier);
            soldier.player = this;
            soldier.transform.position = SpawnPoint();
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Count"))
        {
            int addCount = other.GetComponent<CountObject>().CalculateCount(soldierCount);
            soldierCount += addCount;
            CreateSoldier(addCount);
        }
    }
    private void OnDrawGizmos()
    {
        Color color = Color.green;
        color.g = 0.8f;
        Gizmos.color = color;
        Gizmos.DrawCube(spawnPointBounds.center, spawnPointBounds.size);
    }
}
