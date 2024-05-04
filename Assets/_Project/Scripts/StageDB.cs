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
/// level : 스테이지 레벨 , type : 장애물, Count , type2 : object 종류 , value : 장애물, Count의 계수
/// </summary>
[Serializable]
public class Stage
{
    public int level;
    public int type;
    public int type2;
    public int value;
}


