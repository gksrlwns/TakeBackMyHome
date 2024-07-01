using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ObstacleType { Barrier,Barrier2, Spike, MovableSpin, SpinBlade, Hammer };
public enum CountType { Add, Multiply };

public enum ObjectSetActiveType {Left, Rigth, Both };

public class ObjectData : MonoBehaviour
{
    public ObstacleType obstacle;

    public ObjectSetActiveType position;

    public GameObject[] leftRightObjects;

    public int value;

    public void SetPosition(int index)
    {
        transform.position = new Vector3(0 , 0, index * 20f );
    }
    
    public void SetActiveSelf(ObjectSetActiveType setActiveType)
    {
        switch (setActiveType)
        {
            case ObjectSetActiveType.Left:
            case ObjectSetActiveType.Rigth:
                if(obstacle.Equals(ObstacleType.SpinBlade))
                {
                    leftRightObjects[0].SetActive(true);
                    break;
                }
                for (int i = (int)setActiveType; i < leftRightObjects.Length; i += 2)
                {
                    leftRightObjects[i].SetActive(true);
                }
                break;
            case ObjectSetActiveType.Both:
                for (int i = 0; i < leftRightObjects.Length; i++)
                {
                    leftRightObjects[i].SetActive(true);
                }
                break;
        }
    }

}
