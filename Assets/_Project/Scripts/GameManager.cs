using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public Player_Input player;

    int count;

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
            GameObject obj =  poolManager.GetObject(0);
            var sol = obj.GetComponent<Soldier>();
            sol.player = player;
            obj.transform.parent = player.transform;
            obj.transform.position = player.spawnPos[count].position;
            count++;
            if(count >= player.spawnPos.Length) count = 0;
        }
    }
}
