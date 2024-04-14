using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{ 
    public Transform soldierTr;
    public int count;

    [SerializeField] Bounds enterPointBounds;
    [SerializeField] float speed;

    private Vector2 inputVec;

    

    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec.normalized * Time.deltaTime * speed * 3f;
        Vector3 forVec = Vector3.forward * Time.deltaTime * speed;
        transform.Translate(nextVec + forVec);
    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }
    private void OnDrawGizmos()
    {
        Color color = Color.green;
        color.g = 0.8f;
        Gizmos.color = Color.green;
        Matrix4x4 matrix = transform.localToWorldMatrix;
        matrix.SetTRS(matrix.GetPosition(), matrix.rotation, Vector3.one);

        Gizmos.matrix = matrix;
        Gizmos.DrawCube(enterPointBounds.center, enterPointBounds.size);
    }

    //Soldier의 수에 따라 Bounds의 크기를 키우고 Bounds의 끝에 생성되도록 하면 될 듯
    public Vector3 EnterPoint()
    {
        float randomPointX = transform.position.x + enterPointBounds.center.x + UnityEngine.Random.Range(enterPointBounds.extents.x * -0.5f, enterPointBounds.extents.x * 0.5f);
        //float randomPointY = transform.position.y + enterPointBounds.center.y + UnityEngine.Random.Range(enterPointBounds.extents.y * -0.5f, enterPointBounds.extents.y * 0.5f);
        float randomPointZ = transform.position.z + enterPointBounds.center.z + UnityEngine.Random.Range(enterPointBounds.extents.z * -0.5f, enterPointBounds.extents.z * 0.5f);

        Vector3 spawnPos = new Vector3(randomPointX, 1f, randomPointZ);
        return spawnPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Count"))
        {
            int addCount = other.GetComponent<CountObject>().count;
            count += addCount;
            for(int i = 0; i < addCount; i++)
            {
                GameObject obj = GameManager.instance.poolManager.GetObject(0);
                var sol = obj.GetComponent<Soldier>();
                sol.player = this;
                obj.transform.parent = soldierTr;
                obj.transform.position = EnterPoint();
            }
            
        }
    }



}
