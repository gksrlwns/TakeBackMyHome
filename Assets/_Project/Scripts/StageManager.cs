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
                objectData = Instantiate(obstaclePrefabs[list[i].type2],transform).GetComponent<ObjectData>();
                objectData.SetActiveSelf(list[i].position);
            }
            else
            {
                objectData = Instantiate(countPrefab,transform).GetComponent<ObjectData>();
                objectData.leftRightObjects[0].GetComponent<CountObject>().value = list[i].value;
                objectData.leftRightObjects[1].GetComponent<CountObject>().value = list[i].value2;
                objectData.SetActiveSelf(list[i].position);

            }

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

            StageData stageData = new StageData(stageDB.Stage[i].type, stageDB.Stage[i].type2, stageDB.Stage[i].position, stageDB.Stage[i].value, stageDB.Stage[i].value2);
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
    public int position;
    public int value;
    public int value2;

    public StageData(int type, int type2, int position, int value, int value2)
    {
        this.type = type;
        this.type2 = type2;
        this.position = position;
        this.value = value;
        this.value2 = value2;
    }
}