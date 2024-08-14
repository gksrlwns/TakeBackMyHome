using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 10f;
    [SerializeField] float sideSpeed = 5f;
    private Vector2 inputVec;
    public bool isArrive;
    Camera mainCamera;
    Vector2 touchStartPosition;
    Vector2 touchCurrentPosition;
    Vector3 moveDirection;

    private void Awake()
    {
        mainCamera = Camera.main;
        Debug.Log($"Input Vector: {inputVec}");
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.isPause) return;
        if (isArrive) return;
#if UNITY_ANDROID
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            touchCurrentPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            if (touchStartPosition == Vector2.zero)
            {
                // ��ġ�� ���۵� ��ġ�� ���
                touchStartPosition = touchCurrentPosition;
            }

            // ��ġ �̵� ���� ���� ���
            Vector2 touchDirection = touchCurrentPosition - touchStartPosition;

            // X�� �̵� ���� ���
            Vector3 sideVec = new Vector3(touchDirection.x, 0, 0).normalized * sideSpeed * Time.deltaTime;


            // �÷��̾� �̵�
            transform.Translate(sideVec);
        }
        else
        {
            // ��ġ�� ������ ���� ��ġ �ʱ�ȭ
            touchStartPosition = Vector2.zero;
        }
#endif
#if UNITY_STANDALONE
        Vector3 editorSide = inputVec.normalized * Time.deltaTime * sideSpeed;
        transform.Translate(editorSide);
#endif
        Vector3 forVec = Vector3.forward * Time.deltaTime * forwardSpeed;
        transform.Translate (forVec);

        Vector3 targetPosition = transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -7f, 7f); // ���� ����
        transform.position = targetPosition;

    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }




}
