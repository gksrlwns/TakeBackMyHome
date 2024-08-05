using UnityEngine;

public class FinishObjcet : ObjectData
{
    [SerializeField] Transform soldierFirstPos;
    [SerializeField] NavMeshManager navMeshManager;
    [SerializeField] SpawnArea[] spawnAreas;

    private void Awake() => navMeshManager = GetComponent<NavMeshManager>();

    public void NavMeshSetUp() => navMeshManager.LoadNavMesh();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.player.ArriveDestination(soldierFirstPos.position);
        }
    }
}
