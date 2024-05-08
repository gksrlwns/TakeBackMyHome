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
/// level : �������� ���� , type : ��ֹ�, Count , type2 : object ���� , value : ��ֹ�, Count�� ���
/// </summary>
[Serializable]
public class Stage
{
    public int level;
    public int type;
    public int type2;
    public int value;
}

