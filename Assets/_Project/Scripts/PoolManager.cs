using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType { Soldier, Zombie }

public class PoolManager : MonoBehaviour
{
    [Serializable]
    public class PoolData
    {
        public PoolType poolType;
        public int size;
        public GameObject prefab;
    }
    public GameObject[] prefabs;
    public List<GameObject>[] pools;
    public Queue<GameObject>[] poolQueues;
    [SerializeField] private PoolData[] poolDatas;

    private void Awake()
    {
        poolQueues = new Queue<GameObject>[poolDatas.Length];
        for(int i = 0; i < poolQueues.Length; i++)
        {
            poolQueues[i] = new Queue<GameObject>();
            CreateObject(poolDatas[i], poolQueues[i]);
        }
        pools = new List<GameObject>[prefabs.Length];
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        
    }
    #region QUEUE
    private GameObject CreateObject(PoolData pooldata, Queue<GameObject> queue)
    {
        var obj = Instantiate(pooldata.prefab, transform);
        obj.SetActive(false);
        queue.Enqueue(obj);

        return obj;
    }

    public GameObject SpawnObject(PoolType poolType)
    {
        GameObject obj = null;

        var queue = poolQueues[(int)poolType];
        if(queue.Count <= 0)
        {
            PoolData poolData = poolDatas[(int)poolType];
            CreateObject(poolData, queue);
        }

        obj = queue.Dequeue();
        obj.SetActive(true);


        return obj;
    }
    #endregion

    public GameObject GetObject(int index)
    {
        GameObject obj = null;

        foreach(var temp in pools[index])
        {
            if(!temp.activeSelf)
            {
                obj = temp;
                obj.SetActive(true);
                break;
            }
        }

        if(obj == null)
        {
            obj = Instantiate(prefabs[index]);
            pools[index].Add(obj);
        }

        return obj;
    }
}
