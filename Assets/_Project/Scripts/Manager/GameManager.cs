using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public bool isPause;
    [SerializeField]int startingSoldierCount = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
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
