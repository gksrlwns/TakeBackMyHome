using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
    



}
