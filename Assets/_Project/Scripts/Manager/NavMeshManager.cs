using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] bool isTrig;
    public NavMeshSurface navMeshSurface;
    /// <summary>
    /// ScriptableObject : NavMeshSurface�� �ҷ����� ���� ��ũ���ͺ������Ʈ
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
