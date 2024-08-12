using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Soldiers Info")]
    public int soldierCount;
    public List<Soldier> soldierList;
    [SerializeField] Transform soldierTr;
    SpawnArea spawnArea;

    [Header("Player Info")]
    PlayerController playerController;
    TargetSearch targetSearch;

    public Vector3 targetPos;
    public Vector3 soldierFirstPos;

    private void Awake()
    {
        soldierList = new List<Soldier>();
        playerController = GetComponent<PlayerController>();
        spawnArea = GetComponent<SpawnArea>();
        targetSearch = GetComponent<TargetSearch>();
    }

    private void Start()
    {
        targetSearch.enabled = false;
    }

    #region Sort Soldier
    public void ArriveDestination(Vector3 position)
    {
        playerController.isArrive = true;
        targetSearch.enabled = true;
        Vector3 soldierFirstPos = position;
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

    #region Create Soldier

    public void CreateSoldier(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Soldier soldier = PoolManager.instance.GetPool<Soldier>(PoolType.Soldier, true);
            
            soldier.transform.parent = soldierTr;
            soldierList.Add(soldier);
            soldierCount++;
            soldier.player = this;
            soldier.transform.position = spawnArea.SpawnPoint();
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

}
