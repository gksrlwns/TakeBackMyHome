using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExcelAsset]
public class StageDB : ScriptableObject
{
    public List<Stage> Stage; // Replace 'EntityType' to an actual type that is serializable.
}
/// <summary>
/// level : �������� ���� 
/// objectType : ���� (��ֹ�, Count) 0 : Count, 1 : Obstacle, 2 : Finish Line
/// obstacleType : object ���� (ObstacleType, CountType)
/// position : object ��ġ (0 : Right, 1: Left ,2 : Both)
/// value : ��ֹ�, Count�� ��� (��ֹ� : ��ֹ��� �ӵ� or Finish �ִ� ���� ����, Count : Count ���)
/// value2 : Both�� ���, ��ֹ�, Count�� ��� (��ֹ� : ��ֹ��� �ӵ�, Count : Count ���)
/// </summary>
[Serializable]
public class Stage
{
    public int level;
    public int objectType;
    public int obstacleType;
    public int position;
    public int value;
    public int value2;
}


