using System.Collections.Generic;
using UnityEngine;

public class BlockWayPoints : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private List<Transform> _wayPoints = new List<Transform>();
    [SerializeField] private int _wayPointIndex = 0;
    [SerializeField] private bool _isLoop = true;
    [SerializeField] private bool _isRandom = false;
    [SerializeField] private bool _isActive = false;

    [Header("Block Parameters")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _arriveDistance = 0.01f;
    [SerializeField] private int _damage = 10;

    [Header("Player referments")]
    [SerializeField] private PlayerController _Player;

    private Rigidbody _rb;

    Vector3 direction;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (_Player == null)
        {
            _Player = FindFirstObjectByType<PlayerController>();
        }
    }

    private void Update()
    {
        if (!_isActive) _rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            if (_wayPoints == null || _wayPoints.Count == 0) return;
            if (_wayPoints[_wayPointIndex] == null) return;

            Vector3 targetPoint = _wayPoints[_wayPointIndex].position;

            direction = (targetPoint - _rb.position);
            direction.y = 0f;

            float distance = direction.magnitude;

            if (distance < _arriveDistance)
            {
                _rb.velocity = Vector3.zero;
                if (_isRandom)
                {
                    _wayPointIndex = Random.Range(0, _wayPoints.Count);
                }
                else
                {
                    _wayPointIndex++;
                    if (_isLoop && _wayPointIndex >= _wayPoints.Count) _wayPointIndex = 0;
                }
                return;
            }

            direction.Normalize();

            _rb.velocity = direction * _moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player)) return;

        if (collision.collider.attachedRigidbody != null)
        {
            if (collision.gameObject.TryGetComponent<LifeController>(out LifeController life))
            {
                _Player.PlayerHitByObject();
                life.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_wayPoints == null || _wayPoints.Count == 0) return;
        float radiusWayPoint = 0.1f;
        Gizmos.color = Color.blue;
        for (int i = 0; i < _wayPoints.Count; i++)
        {
            if (_wayPoints[i] == null) continue;
            Gizmos.DrawWireSphere(_wayPoints[i].position, radiusWayPoint);
        }
    }
}