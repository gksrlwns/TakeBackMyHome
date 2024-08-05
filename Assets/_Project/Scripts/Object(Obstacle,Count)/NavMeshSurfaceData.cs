using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NavMeshData", menuName = "ScriptableObjcet/NavMeshData")]
public class NavMeshSurfaceData : ScriptableObject
{
    [SerializeField] NavMeshData navMeshData;
    public NavMeshData NavMeshData { get { return navMeshData; } set { navMeshData = value; } }
}
