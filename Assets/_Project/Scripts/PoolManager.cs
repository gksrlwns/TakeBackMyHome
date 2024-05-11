using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    [Serializable]
    public class PoolType
    {
        public int size;
        public GameObject prefab;
    }
    public GameObject[] prefabs;
    public List<GameObject>[] pools;
    [SerializeField] private int initCount;
    [SerializeField] private PoolType[] poolTypes;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        
    }

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
