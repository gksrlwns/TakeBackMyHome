using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] bool isTrig;
    public NavMeshSurface navMeshSurface;
    /// <summary>
    /// ScriptableObject : NavMeshSurface를 불러오기 위한 스크립터블오브젝트
    /// </summary>
    [SerializeField] NavMeshSurfaceData navMeshSurfaceData;

    private void OnValidate()
    {
        if (isTrig)
        {
            navMeshSurface.BuildNavMesh();
            navMeshSurfaceData.NavMeshData = navMeshSurface.navMeshData;
            isTrig = false;
        }
    }

    public void LoadNavMesh()
    {
        if(navMeshSurfaceData != null)
        {
            navMeshSurface.navMeshData = navMeshSurfaceData.NavMeshData;
            navMeshSurface.BuildNavMesh();
        }
    }
}
