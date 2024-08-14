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
                // 터치가 시작된 위치를 기록
                touchStartPosition = touchCurrentPosition;
            }

            // 터치 이동 방향 벡터 계산
            Vector2 touchDirection = touchCurrentPosition - touchStartPosition;

            // X축 이동 벡터 계산
            Vector3 sideVec = new Vector3(touchDirection.x, 0, 0).normalized * sideSpeed * Time.deltaTime;


            // 플레이어 이동
            transform.Translate(sideVec);
        }
        else
        {
            // 터치가 끝나면 시작 위치 초기화
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
        targetPosition.x = Mathf.Clamp(targetPosition.x, -7f, 7f); // 예시 범위
        transform.position = targetPosition;

    }

    void OnMove(InputValue input)
    {
        inputVec = input.Get<Vector2>();
    }




}
