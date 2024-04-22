using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageDB stageDB;

    private void Start()
    {
        for (int i = 0; i < stageDB.Stage.Count; i++)
        {
            Debug.Log($"Level : {stageDB.Stage[i].level}, Type : {stageDB.Stage[i].type}, Type 2 : {stageDB.Stage[i].type2}, Value : {stageDB.Stage[i].value}");
        }
    }

    public void CreateStage(int level)
    {
        for (int i = 0; i < stageDB.Stage.Count; i++)
        {
            if (stageDB.Stage[i].level != level) continue;
        }
    }
}
