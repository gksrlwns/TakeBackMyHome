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
        StartCoroutine(Check());
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
        Gizmos.color = color;
        //Matrix4x4 matrix = transform.localToWorldMatrix;
        //matrix.SetTRS(matrix.GetPosition(), matrix.rotation, Vector3.one);

        //Gizmos.matrix = matrix;
        Gizmos.DrawCube(spawnPointBounds.center, spawnPointBounds.size);

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
