using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleObjectController))]
public class EditorObjectDataController : Editor
{
    public override void OnInspectorGUI()
    {
        // Ÿ�� ������Ʈ�� CountObject�� ĳ����
        ObstacleObjectController countObject = (ObstacleObjectController)target;

        // ���� Ŭ���� �ʵ带 ���� ǥ��
        EditorGUILayout.LabelField("Count Object Fields", EditorStyles.boldLabel);
        
        //countObject. = EditorGUILayout.IntField("Count Value 1", countObject.countValue1);
        //countObject.countValue2 = EditorGUILayout.IntField("Count Value 2", countObject.countValue2);

        // ���� Ŭ������ �⺻ �ν����͸� ǥ��
        EditorGUILayout.LabelField("Base Object Fields", EditorStyles.boldLabel);
        base.OnInspectorGUI();
    }
}
