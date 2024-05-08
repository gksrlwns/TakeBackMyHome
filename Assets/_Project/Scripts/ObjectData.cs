using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    enum ObstacleType { Barrier, Spike, MovableSpin, SpinBlade, Hammer  };
    enum CountType { Add, Multiply };

    public GameObject[] leftRightObjects;

    public StageData stageData;
    
    public void DebugData()
    {
        Debug.Log($"name : {this.name}");
        Debug.Log($"type : {stageData.type}");
        Debug.Log($"type2: {stageData.type2}");
        Debug.Log($"value: {stageData.value}");
    }
    
    public void SetPosition(int index)
    {
        transform.position = new Vector3(0 , 0, index * 12f );
    }

    public void SetActiveSelf(int index)
    {
        for (int i = index; i < leftRightObjects.Length; i += 2)
        {
            leftRightObjects[i].SetActive(true);
        }
    }

}
