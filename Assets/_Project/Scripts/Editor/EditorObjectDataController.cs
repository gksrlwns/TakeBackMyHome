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
        ObstacleObjectController obstacleObjectController = (ObstacleObjectController)target;

        obstacleObjectController.obstacle = (ObstacleType)EditorGUILayout.EnumPopup("Obstacle Type", obstacleObjectController.obstacle);
        
        base.OnInspectorGUI();
    }
}
