using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    CharacterController controller;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec.normalized * Time.deltaTime * speed * 3f;
        Vector3 forVec = Vector3.forward * Time.deltaTime * speed;
        transform.Translate(nextVec + forVec);
        //controller.Move(nextVec + forVec);
    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }
    
}
