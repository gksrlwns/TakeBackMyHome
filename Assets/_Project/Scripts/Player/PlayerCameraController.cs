using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Transform playerTr;
    [SerializeField] Vector3 initPos;
    [SerializeField] float offSet;

    private void Awake()
    {
        initPos = virtualCamera.transform.position;
    }
    private void LateUpdate()
    {
        Vector3 cameraPos = virtualCamera.transform.position;

        cameraPos.x = initPos.x;
        cameraPos.y = initPos.y;
        cameraPos.z = playerTr.position.z + offSet;

        virtualCamera.transform.position = cameraPos;
    }
}
