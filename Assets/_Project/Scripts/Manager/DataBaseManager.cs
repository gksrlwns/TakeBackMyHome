using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    static DataBaseManager instance;
    public static DataBaseManager Instance { get { return instance; } }

    [SerializeField] SoldierData soldierData;
    public PlayerData playerData;

    const string PlayerDataKey = "PlayerData";
    [SerializeField] const int CURRENT_VERSION = 1;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        playerData = LoadPlayerData();
    }

    public void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PlayerDataKey, json);
        PlayerPrefs.Save();
    }

    public PlayerData LoadPlayerData()
    {
        PlayerData playerData;
        if (PlayerPrefs.HasKey(PlayerDataKey) )
        {
            string json = PlayerPrefs.GetString(PlayerDataKey);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            if(playerData.version != CURRENT_VERSION || playerData.version == 0)
            {
                Debug.Log("PlayerData Version이 다름");
                playerData = CreatePlayerData();
            }
        }
        else
        {
            Debug.Log("PlayerData가 없음 -> 새로 생성");
            playerData = CreatePlayerData();
        }

        return playerData;
    }
    
    PlayerData CreatePlayerData()
    {
        var playerData = new PlayerData
        {
            version = CURRENT_VERSION,
            stageLevel = 1,
            coin = 0,

            soldierStatus = new SoldierStatus
            {
                attackSpeed = soldierData.AttackSpeed,
                damage = soldierData.Damage,
                maxHp = soldierData.MaxHp
            }
        };
        SavePlayerData(playerData);

        return playerData;
    }
    

}

[System.Serializable]
public struct PlayerData
{
    public int version;
    public int stageLevel;
    public int coin;
    public SoldierStatus soldierStatus;
    
}


[System.Serializable]
public struct SoldierStatus
{
    public float damage;
    public float attackSpeed;
    public float maxHp;
}

