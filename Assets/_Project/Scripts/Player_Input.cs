using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    public Vector2 inputVec;
    Rigidbody rigid;
    public float speed;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec.normalized * Time.deltaTime * speed;
        Vector3 forVec = Vector3.forward * Time.deltaTime * speed;
        rigid.MovePosition(rigid.position + nextVec + forVec);
    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }
    
}
