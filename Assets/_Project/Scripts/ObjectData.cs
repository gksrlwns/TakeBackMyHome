using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ObstacleType { Barrier, Spike, MovableSpin, SpinBlade, Hammer };
public enum CountType { Add, Multiply };

public class ObjectData : MonoBehaviour
{
    public ObstacleType obstacle;

    public GameObject[] leftRightObjects;

    public void SetPosition(int index)
    {
        transform.position = new Vector3(0 , 0, index * 16f );
    }

    public void SetActiveSelf(int index)
    {
        if (leftRightObjects.Length.Equals(1))
        {
            leftRightObjects[0].SetActive(true);
            return;
        }
        if(index.Equals(2))
        {
            for(int i = 0; i < leftRightObjects.Length; i++)
            {
                leftRightObjects[i].SetActive(true);
            }
            return;
        }

        for (int i = index; i < leftRightObjects.Length; i += 2)
        {
            leftRightObjects[i].SetActive(true);
        }
    }

}
