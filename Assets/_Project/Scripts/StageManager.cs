using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageDB stageDB;
    public Dictionary<int, List<StageData>> stageDataDict;
    [SerializeField] ObjectData[] objectPrefabs;

    private void Awake()
    {
        List<StageData> list = new List<StageData>();
        int index = 1;
        for (int i = 0; i < stageDB.Stage.Count; i++)
        {
            Debug.Log("Dictionary 생성");
            if(index != stageDB.Stage[i].level)
            {
                stageDataDict.Add(stageDB.Stage[i].level, list);
                Debug.Log($"Dictionary 추가 {stageDB.Stage[i].level}");
                index = stageDB.Stage[i].level;
                Debug.Log($"Dictionary index 변경 {index}");
                list.Clear();
                Debug.Log("List 초기화");
            }

            StageData stageData = new StageData(stageDB.Stage[i].type, stageDB.Stage[i].type2, stageDB.Stage[i].value);
            list.Add(stageData);
            Debug.Log($"StageData List 추가");
        }
        
    }
    private void Start()
    {
        //for (int i = 0; i < stageDB.Stage.Count; i++)
        //{
        //    Debug.Log($"Level : {stageDB.Stage[i].level}, Type : {stageDB.Stage[i].type}, Type 2 : {stageDB.Stage[i].type2}, Value : {stageDB.Stage[i].value}");
        //}
        foreach(var item in stageDataDict)
        {
            Debug.Log(item.Key + ": " + item.Value);
        }
    }

    public void CreateStage(int level)
    {
        List<StageData> list = stageDataDict[level];
        for(int i = 0; i<list.Count; i++)
        {
            objectPrefabs[i].stageData = list[i];
            objectPrefabs[i].DebugData();
        }
    }
}
[Serializable]
public struct StageData
{
    public int type;
    public int type2;
    public int value;

    public StageData(int type, int type2, int value)
    {
        this.type = type;
        this.type2 = type2;
        this.value = value;
    }
}