using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 10f;
    [SerializeField] float sideSpeed = 5f;
    private Vector2 inputVec;
    public bool isArrive;

    private void FixedUpdate()
    {
        if(GameManager.instance.isPause) return;
        if (isArrive) return;
        Vector3 nextVec = inputVec.normalized * Time.deltaTime * sideSpeed;
        Vector3 forVec = Vector3.forward * Time.deltaTime * forwardSpeed;
        transform.Translate(nextVec + forVec);
    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }
    



}
