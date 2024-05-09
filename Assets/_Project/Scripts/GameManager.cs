using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    

    public static GameManager instance;
    public PoolManager poolManager;
    public Player player;

    int count = 2;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < count; i++)
            {
                GameObject obj = poolManager.GetObject(0);
                var sol = obj.GetComponent<Soldier>();
                sol.player = player;
                player.soldierCount++;
                obj.transform.parent = player.soldierTr;
                obj.transform.position = player.SpawnPoint();
            }
            count += 10;
            
        }
    }
}
