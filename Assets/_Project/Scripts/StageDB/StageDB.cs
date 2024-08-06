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
/// type : ���� (��ֹ�, Count) 0 : Count, 1 : Obstacle
/// type2 : object ���� (ObstacleType, CountType)
/// position : object ��ġ (0 : Right, 1: Left ,2 : Both)
/// value : ��ֹ�, Count�� ��� (��ֹ� : ��ֹ��� �ӵ� or Finish ���� ����, Count : Count ���)
/// value2 : Both�� ���, ��ֹ�, Count�� ��� (��ֹ� : ��ֹ��� �ӵ� or Finish ���� ���� �ֱ� , Count : Count ���)
/// </summary>
[Serializable]
public class Stage
{
    public int level;
    public int type;
    public int type2;
    public int position;
    public int value;
    public int value2;
}


