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
        ObstacleObjectController obstacleObjectController = (ObstacleObjectController)target;

        obstacleObjectController.obstacle = (ObstacleType)EditorGUILayout.EnumPopup("Obstacle Type", obstacleObjectController.obstacle);
        
        base.OnInspectorGUI();
    }
}
