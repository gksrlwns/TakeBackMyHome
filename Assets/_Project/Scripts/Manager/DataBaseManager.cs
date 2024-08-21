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


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
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
        if ( PlayerPrefs.HasKey(PlayerDataKey) )
        {
            string json = PlayerPrefs.GetString(PlayerDataKey);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.Log("PlayerData가 없음 -> 새로 생성");
            playerData = new PlayerData
            {
                stageLevel = 1,
                coin = 0
            };
            SavePlayerData(playerData);
        }

        return playerData;
    }
    
    public SoldierData LoadSoldierData()
    {
        return soldierData;
    }
    

}

[System.Serializable]
public struct PlayerData
{
    public int stageLevel;
    public int coin;
    
}

