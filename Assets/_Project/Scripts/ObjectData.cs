using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    enum ObstacleType { Barrier, Spike, MovableSpin, SpinBlade, Hammer  };
    enum CountType { Add, Multiply };

    public StageData stageData;

    public void DebugData()
    {
        Debug.Log($"name : {this.name}");
        Debug.Log($"type : {stageData.type}");
        Debug.Log($"type2: {stageData.type2}");
        Debug.Log($"value: {stageData.value}");
    }

}
