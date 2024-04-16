using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform soldierTr;
    public int count;

    [SerializeField] Bounds spawnPointBounds;
    WaitForSecondsRealtime waitTime = new WaitForSecondsRealtime(1);


    private void Start()
    {
        
    }

    IEnumerator Check()
    {
        Debug.Log($"Center : {spawnPointBounds.center}");
        Debug.Log($"Extends : {spawnPointBounds.extents}");
        Debug.Log($"Max : {spawnPointBounds.max}");
        Debug.Log($"Min : {spawnPointBounds.min}");
        yield return waitTime;
        StartCoroutine(Check());
    }
    private void OnDrawGizmos()
    {
        Color color = Color.green;
        color.g = 0.8f;
        Gizmos.color = Color.green;
        Matrix4x4 matrix = transform.localToWorldMatrix;
        matrix.SetTRS(matrix.GetPosition(), matrix.rotation, Vector3.one);

        Gizmos.matrix = matrix;
        Gizmos.DrawCube(spawnPointBounds.center, spawnPointBounds.size);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(spawnPointBounds.max, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPointBounds.min, 0.1f);

        Gizmos.color = Color.yellow;
        Vector3 pos = new Vector3(spawnPointBounds.extents.x, spawnPointBounds.extents.y, spawnPointBounds.extents.z);
        Gizmos.DrawSphere(pos, 0.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(-spawnPointBounds.extents.x, spawnPointBounds.extents.y, spawnPointBounds.extents.z), 0.1f);


    }

    //Soldier의 수에 따라 Bounds의 크기를 키우고 Bounds의 끝에 생성되도록 하면 될 듯
    public Vector3 SpawnPoint2()
    {
        

        Vector3 spawnPos = new Vector3();
        return spawnPos;
    }

    public Vector3 SpawnPoint()
    {
        float randomPointX = transform.position.x + spawnPointBounds.center.x + UnityEngine.Random.Range(spawnPointBounds.extents.x * -0.5f, spawnPointBounds.extents.x * 0.5f);
        //float randomPointY = transform.position.y + enterPointBounds.center.y + UnityEngine.Random.Range(enterPointBounds.extents.y * -0.5f, enterPointBounds.extents.y * 0.5f);
        float randomPointZ = transform.position.z + spawnPointBounds.center.z + UnityEngine.Random.Range(spawnPointBounds.extents.z * -0.5f, spawnPointBounds.extents.z * 0.5f);

        Vector3 spawnPos = new Vector3(randomPointX, 0.5f, randomPointZ);
        return spawnPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Count"))
        {
            int addCount = other.GetComponent<CountObject>().count;
            count += addCount;
            for (int i = 0; i < addCount; i++)
            {
                GameObject obj = GameManager.instance.poolManager.GetObject(0);
                var sol = obj.GetComponent<Soldier>();
                sol.player = this;
                obj.transform.parent = soldierTr;
                obj.transform.position = SpawnPoint();
            }

        }
    }
}
