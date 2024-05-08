using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageDB stageDB;
    [SerializeField] Dictionary<int, List<StageData>> stageDataDict;
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] GameObject countPrefab;
    
    private void Awake()
    {
        StageSeparation();
        CreateStage(1);
    }
    

    public void CreateStage(int level)
    {
        List<StageData> list = stageDataDict[level];
        for (int i = 0; i < list.Count; i++)
        {
            ObjectData objectData = null;
            if (list[i].type.Equals(1))
            {
                objectData = Instantiate(obstaclePrefabs[list[i].type2]).GetComponent<ObjectData>();
                objectData.SetActiveSelf(0);
            }
            else
            {
                objectData = Instantiate(countPrefab).GetComponent<ObjectData>();
                objectData.leftRightObjects[0].GetComponent<CountObject>().count = list[i].value;
                objectData.SetActiveSelf(0);
                
            }

            objectData.DebugData();
            objectData.SetPosition(i+1);
        }
    }
    /// <summary>
    /// 게임시작 시 한번만 분리하도록 추후에 변경
    /// </summary>
    void StageSeparation()
    {
        stageDataDict = new Dictionary<int, List<StageData>>();
        List<StageData> list = new List<StageData>();
        int index = 1;
        for (int i = 0; i < stageDB.Stage.Count; i++)
        {
            if (index != stageDB.Stage[i].level)
            {
                stageDataDict.Add(stageDB.Stage[i].level, list);
                index = stageDB.Stage[i].level;
                list.Clear();
            }

            StageData stageData = new StageData(stageDB.Stage[i].type, stageDB.Stage[i].type2, stageDB.Stage[i].value);
            list.Add(stageData);
            if (i.Equals(stageDB.Stage.Count - 1)) stageDataDict.Add(stageDB.Stage[i].level, list);
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