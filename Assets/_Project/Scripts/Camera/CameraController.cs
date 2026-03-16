using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _mouseSensitivity = 5f;
    [SerializeField] float bottomClamp = -20f;
    [SerializeField] float topClamp = 60f;
    [SerializeField] float _maxZoomDistance = 10f;
    [SerializeField] float _minZoomDistance = 2f;
    [SerializeField] Vector3 offset = new Vector3(0, 2, -5);
    [SerializeField] private float _startYaw = 0f;
    [SerializeField] private float _startPitch = 0f;
    [SerializeField] private float _sphereRadius = 0.2f;
    [SerializeField] private float _minOffestFromWall = 0.1f;
    [SerializeField] private LayerMask _groundMask;

    private float orbitRadius = 5f;

    private Vector3 lookAt;

    private float _yaw = 0f;
    private float _pitch = 0f;

    private void Start()
    {
        _yaw = _startYaw;
        _pitch = _startPitch;
        _pitch = Mathf.Clamp(_pitch, bottomClamp, topClamp);
    }

    public void SetCameraSettings(float minClamp, float maxClamp)
    {
        bottomClamp = minClamp;
        topClamp = maxClamp;
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        if (Input.GetMouseButton(1))
        {
            _yaw += Input.GetAxis("Mouse X") * _mouseSensitivity;
            _pitch -= Input.GetAxis("Mouse Y") * _mouseSensitivity;

            _pitch = Mathf.Clamp(_pitch, bottomClamp, topClamp);

            Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
            Vector3 desiredPosition = _target.position + rotation * offset;

            lookAt = _target.position + Vector3.up * 2;
            Quaternion lookRotation = Quaternion.LookRotation(lookAt - desiredPosition);
            transform.SetPositionAndRotation(desiredPosition, lookRotation);
        }

        orbitRadius -= Input.mouseScrollDelta.y / _mouseSensitivity;
        orbitRadius = Mathf.Clamp(orbitRadius, _minZoomDistance, _maxZoomDistance);

        Vector3 pivotTarget = _target.position + Vector3.up * 2;
        Vector3 desiredPos = _target.position - transform.forward * orbitRadius;
        Vector3 direction = desiredPos - pivotTarget;
        float distance = direction.magnitude;
        if (distance > 0.001f)
        {
            direction /= distance;

            if (Physics.SphereCast(pivotTarget, _sphereRadius, direction, out RaycastHit hit, distance, _groundMask, QueryTriggerInteraction.Ignore))
            {
                float safeDist = Mathf.Max(0f, hit.distance - _minOffestFromWall);
                desiredPos = pivotTarget + direction * safeDist;
            }
        }

        transform.position = desiredPos;
    }
}
