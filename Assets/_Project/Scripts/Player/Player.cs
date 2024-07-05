using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int soldierCount;
    public List<Soldier> soldierList;
    [SerializeField] Transform soldierTr;
    [SerializeField] Bounds spawnPointBounds;
    WaitForSecondsRealtime waitTime = new WaitForSecondsRealtime(1);


    private void Awake()
    {
        soldierList = new List<Soldier>();
    }

    private void OnDrawGizmos()
    {
        Color color = Color.green;
        color.g = 0.8f;
        Gizmos.color = color;
        //Matrix4x4 matrix = transform.localToWorldMatrix;
        //matrix.SetTRS(matrix.GetPosition(), matrix.rotation, Vector3.one);

        //Gizmos.matrix = matrix;
        Gizmos.DrawCube(spawnPointBounds.center, spawnPointBounds.size);

    }

    public Vector3 SpawnPoint()
    {
        float randomPointX = transform.position.x + spawnPointBounds.center.x + UnityEngine.Random.Range(spawnPointBounds.extents.x * -0.5f, spawnPointBounds.extents.x * 0.5f);
        float randomPointZ = transform.position.z + spawnPointBounds.center.z + UnityEngine.Random.Range(spawnPointBounds.extents.z * -0.5f, spawnPointBounds.extents.z * 0.5f);

        Vector3 spawnPos = new Vector3(randomPointX, 0.5f, randomPointZ);
        return spawnPos;
    }

    public void CreateSoldier(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject obj = PoolManager.instance.GetObject(PoolType.Soldier, true);
            
            obj.transform.parent = soldierTr;
            var soldier = obj.GetComponent<Soldier>();
            soldierList.Add(soldier);
            soldier.player = this;
            obj.transform.position = SpawnPoint();
        }
    }
    IEnumerator Check()
    {
        Debug.Log($"Center : {spawnPointBounds.center}");
        Debug.Log($"Extends : {spawnPointBounds.extents}");
        Debug.Log($"Max : {spawnPointBounds.max}");
        Debug.Log($"Min : {spawnPointBounds.min}");
        yield return waitTime;
        StartCoroutine(Check());
    }

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
