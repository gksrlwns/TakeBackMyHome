using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleObjectController))]
public class EditorObjectDataController : Editor
{
    public override void OnInspectorGUI()
    {
        // 타겟 오브젝트를 CountObject로 캐스팅
        ObstacleObjectController countObject = (ObstacleObjectController)target;

        // 하위 클래스 필드를 먼저 표시
        EditorGUILayout.LabelField("Count Object Fields", EditorStyles.boldLabel);
        
        //countObject. = EditorGUILayout.IntField("Count Value 1", countObject.countValue1);
        //countObject.countValue2 = EditorGUILayout.IntField("Count Value 2", countObject.countValue2);

        // 상위 클래스의 기본 인스펙터를 표시
        EditorGUILayout.LabelField("Base Object Fields", EditorStyles.boldLabel);
        base.OnInspectorGUI();
    }
}
