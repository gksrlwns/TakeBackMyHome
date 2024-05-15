using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public bool isStop;
    [SerializeField]int startingSoldierCount = 5;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        if(isStop) return;
        player.CreateSoldier(startingSoldierCount);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    GameObject obj = PoolManager.instance.GetObject(PoolType.Zombie);
        //    obj.transform.position = Vector3.zero;
        //}
    }
}
