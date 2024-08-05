using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageDB stageDB;   //Excel���� ��ü -> StageDB(Excel) �� Stage(Sheet)�� Stage�� ���� ������
    [SerializeField] Dictionary<int, List<StageData>> stageDataDict;
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] GameObject countPrefab;
    [SerializeField] GameObject emptyPrefab;
    [SerializeField] GameObject finishLinePrefab;
    [SerializeField] int level = 1;
    [SerializeField] int emptyCount = 3;
    
    private void Awake()
    {
        CheckStageDB();

        // ���ӽ��� �� �ѹ��� �и��ϵ��� ���Ŀ� ����
        StageSeparation();
        CreateStage(level);
    }
    /// <summary>
    /// Stage�� ���� Data�� �����ִ��� Ȯ��
    /// ���� �߻� �� ������ ���� ���� �� ����
    /// </summary>
    void CheckStageDB()
    {
        for (int i = 0; i< stageDB.Stage.Count;i++)
        {
            if(stageDB.Stage[i].type.Equals(0))
            {
                if (stageDB.Stage[i].position > Enum.GetValues(typeof(ObjectSetActiveType)).Length - 1)
                {
                    Debug.Log($"{i}��° Data�� Position ���� ������ ������ ŭ");
                    stageDB.Stage[i].position = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObjectSetActiveType)).Length - 1);
                }
            }
            else if (stageDB.Stage[i].type.Equals(1))
            {
                if (stageDB.Stage[i].type2 > Enum.GetValues(typeof(ObstacleType)).Length - 1)
                {
                    Debug.Log($"{i}��° Data�� Type2 ���� ������ ������ ŭ");
                    stageDB.Stage[i].type2 = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObstacleType)).Length - 1);
                }
                if (!stageDB.Stage[i].type2.Equals((int)ObstacleType.Hammer) && stageDB.Stage[i].position > Enum.GetValues(typeof(ObjectSetActiveType)).Length - 2)
                {
                    Debug.Log($"{i}��° Data�� Position ���� ������ ������ ŭ");
                    stageDB.Stage[i].position = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObjectSetActiveType)).Length - 2);
                }
            }
        }
    }
    /// <summary>
    /// Stage�� Level�� �´� Object�� ����
    /// </summary>
    /// <param name="level"></param>
    public void CreateStage(int level)
    {
        List<StageData> list = stageDataDict[level];
        ObjectData objectData;
        for (int i = 0; i < list.Count; i++)
        {
            //0 : CountObject
            if (list[i].type.Equals(0))
            {
                objectData = Instantiate(countPrefab, transform).GetComponent<ObjectData>();
                objectData.leftRightObjects[0].GetComponent<CountObject>().value = list[i].value;
                objectData.leftRightObjects[1].GetComponent<CountObject>().value = list[i].value2;
            }
            //1 : ObstacleObject
            else
            {
                objectData = Instantiate(obstaclePrefabs[list[i].type2], transform).GetComponent<ObjectData>();
                objectData.obstacle = (ObstacleType)list[i].type2;
                objectData.value = list[i].value;
            }
            objectData.SetActiveSelf((ObjectSetActiveType)list[i].position);

            objectData.SetPosition(i+1);
        }
        for(int i = 1; i <= emptyCount; i++)
        {
            objectData = Instantiate(emptyPrefab, transform).AddComponent<ObjectData>().GetComponent<ObjectData>();
            objectData.SetPosition(list.Count + i);
        }

        FinishObjcet finishObjcet = Instantiate(finishLinePrefab, transform).GetComponent<FinishObjcet>();
        finishObjcet.SetPosition(list.Count + emptyCount + 1);
        finishObjcet.NavMeshSetUp();
    }
    /// <summary>
    /// StageDB���� ��� Stage�� ���� Data�� ���������Ƿ� ������ ���۽� Level�� ���� Dictionary�� ����
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
                stageDataDict.Add(stageDB.Stage[i].level - 1, list);
                index = stageDB.Stage[i].level;
                list.Clear();
            }

            StageData stageData = new StageData(stageDB.Stage[i].type, stageDB.Stage[i].type2, stageDB.Stage[i].position, stageDB.Stage[i].value, stageDB.Stage[i].value2);
            list.Add(stageData);
            if (i.Equals(stageDB.Stage.Count - 1))
            {
                stageDataDict.Add(stageDB.Stage[i].level, list);
            }
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