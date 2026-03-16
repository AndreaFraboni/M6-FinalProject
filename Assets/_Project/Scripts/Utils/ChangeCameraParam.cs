using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraParam : MonoBehaviour
{
    [Header("Camera Ref")]
    [SerializeField] private CameraController _currentCamera;

    [Header("New Camera Settings")]
    [SerializeField] float minClamp = -40.0f;
    [SerializeField] float maxClamp = 0.0f;

    private void Awake()
    {
        if (_currentCamera == null) _currentCamera = FindAnyObjectByType<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            _currentCamera.SetCameraSettings(minClamp, maxClamp);
        }
    }

}
