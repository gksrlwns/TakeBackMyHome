using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] int initPriority = 9;
    [SerializeField] int setPriority = 11;

    private void Awake() => virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    public void InitializeCamera() => virtualCamera.Priority = initPriority;
    public void SetCamera() => virtualCamera.Priority = setPriority;

}
