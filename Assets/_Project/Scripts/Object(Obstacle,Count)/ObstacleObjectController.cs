using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObstacleType { Barrier, Barrier2, Spike, MovableSpin, SpinBlade, Hammer };

public class ObstacleObjectController : ObjectDataController
{
    [HideInInspector] public ObstacleType obstacle;
    [SerializeField] int value;

    public void SetObstacle(int obstacleIndex, int _value)
    {
        obstacle = (ObstacleType)obstacleIndex;
        value = _value;
    }
}
