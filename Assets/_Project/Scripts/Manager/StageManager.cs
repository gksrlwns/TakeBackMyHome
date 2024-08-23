using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance
    {
        get => instance;
    }

    [SerializeField] StageDB stageDB;   //Excel���� ��ü -> StageDB(Excel) �� Stage(Sheet)�� Stage�� ���� ������
    [SerializeField] Dictionary<int, List<StageData>> stageDataDict;
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] GameObject countPrefab;
    [SerializeField] GameObject emptyPrefab;
    [SerializeField] GameObject finishLinePrefab;
    public int level = 1;
    [SerializeField] int emptyCount = 3;
    
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this);

        if (!CheckStageDB())
        {
            Debug.LogWarning("StageDB�� ���� ����");
            return;
        }
        if (stageDataDict == null)
        {
            stageDataDict = new Dictionary<int, List<StageData>>();
        }

        // ���ӽ��� �� �ѹ��� �и�
        StageSeparation();
        
    }

    /// <summary>
    /// Stage�� ���� Data�� �����ִ��� Ȯ��
    /// ���� �߻� �� ������ ���� ���� �� ����
    /// </summary>
    bool CheckStageDB()
    {
        for (int i = 0; i< stageDB.Stage.Count;i++)
        {
            if(stageDB.Stage[i].objectType.Equals(0))
            {
                if (stageDB.Stage[i].position > Enum.GetValues(typeof(ObjectSetActiveType)).Length - 1)
                {
                    Debug.Log($"{i}��° Data�� Position ���� ������ ������ ŭ");
                    stageDB.Stage[i].position = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObjectSetActiveType)).Length - 1);
                    return false;
                }
            }
            else if (stageDB.Stage[i].objectType.Equals(1))
            {
                if (stageDB.Stage[i].obstacleType > Enum.GetValues(typeof(ObstacleType)).Length - 1)
                {
                    Debug.Log($"{i}��° Data�� Type2 ���� ������ ������ ŭ");
                    stageDB.Stage[i].obstacleType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObstacleType)).Length - 1);
                    return false;
                }
                if (!stageDB.Stage[i].obstacleType.Equals((int)ObstacleType.Hammer) && stageDB.Stage[i].position > Enum.GetValues(typeof(ObjectSetActiveType)).Length - 2)
                {
                    Debug.Log($"{i}��° Data�� Position ���� ������ ������ ŭ");
                    stageDB.Stage[i].position = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ObjectSetActiveType)).Length - 2);
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// Stage�� Level�� �´� Object�� ����
    /// </summary>
    public void CreateStage()
    {
        List<StageData> list = stageDataDict[level];
        ObjectDataController objectDataController;
        GameObject parentTr = new GameObject("StageList");
        for (int i = 0; i < list.Count; i++)
        {
            //0 : CountObject
            if (list[i].objectType.Equals(0))
            {
                CountObjcetController countObjcetController = Instantiate(countPrefab, parentTr.transform).GetComponent<CountObjcetController>();
                countObjcetController.SetCountValue(list[i].value, list[i].value2);
                objectDataController = countObjcetController;
            }
            //1 : ObstacleObject
            else
            {
                ObstacleObjectController obstacleObjectController = Instantiate(obstaclePrefabs[list[i].obstacleType], parentTr.transform).GetComponent<ObstacleObjectController>();
                obstacleObjectController.SetObstacle(list[i].obstacleType, list[i].value);
                objectDataController = obstacleObjectController;
            }

            objectDataController.SetActiveSelf((ObjectSetActiveType)list[i].position);

            objectDataController.SetPosition(i+1);
        }
        for(int i = 1; i <= emptyCount; i++)
        {
            objectDataController = Instantiate(emptyPrefab, parentTr.transform).AddComponent<ObjectDataController>().GetComponent<ObjectDataController>();
            objectDataController.SetPosition(list.Count + i);
        }

        FinishObjcetController finishObjcet = Instantiate(finishLinePrefab, parentTr.transform).GetComponent<FinishObjcetController>();
        finishObjcet.SetPosition(list.Count + emptyCount + 1);
        finishObjcet.InitializeSetUp(list[list.Count-1].value, level);
    }
    public void RandomCreateStage()
    {

    }
    /// <summary>
    /// StageDB���� ��� Stage�� ���� Data�� ���������Ƿ� ������ ���۽� Level�� ���� Dictionary�� ����
    /// </summary>
    void StageSeparation()
    {
        List<StageData> list = new List<StageData>();
        int index = 1;
        for (int i = 0; i < stageDB.Stage.Count; i++)
        {
            if (index != stageDB.Stage[i].level)
            {
                stageDataDict.Add(stageDB.Stage[i].level - 1, new List<StageData>(list));
                index = stageDB.Stage[i].level;
                list.Clear();
            }

            StageData stageData = new StageData(stageDB.Stage[i].objectType, stageDB.Stage[i].obstacleType, stageDB.Stage[i].position, stageDB.Stage[i].value, stageDB.Stage[i].value2);
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
    public int objectType;
    public int obstacleType;
    public int position;
    public int value;
    public int value2;

    public StageData(int type, int type2, int position, int value, int value2)
    {
        this.objectType = type;
        this.obstacleType = type2;
        this.position = position;
        this.value = value;
        this.value2 = value2;
    }
}