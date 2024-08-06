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
/// level : 스테이지 레벨 
/// type : 유형 (장애물, Count) 0 : Count, 1 : Obstacle
/// type2 : object 종류 (ObstacleType, CountType)
/// position : object 위치 (0 : Right, 1: Left ,2 : Both)
/// value : 장애물, Count의 계수 (장애물 : 장애물의 속도 or Finish 좀비 숫자, Count : Count 계수)
/// value2 : Both일 경우, 장애물, Count의 계수 (장애물 : 장애물의 속도 or Finish 좀비 생성 주기 , Count : Count 계수)
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


