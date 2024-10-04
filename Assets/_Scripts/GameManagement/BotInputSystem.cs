using System;
using System.Collections;
using UnityEngine;

public class BotInputSystem : MonoBehaviour, InputSystem
{
    Vector3 InputSystem.ShootOrigin { get => _shootOrigin.position; set => throw new NotImplementedException(); }
    Vector3 InputSystem.ShootDirection { get => _shootPoint - _shootOrigin.position; set => throw new NotImplementedException(); }
    Vector3 InputSystem.CharacterRotation { get => _rotation; set => throw new NotImplementedException(); }
    bool InputSystem.IsShooting { get => _isShooting; set => throw new NotImplementedException(); }

    public event Action<Vector2> OnDirectionUpdateAction;
    public event Action OnJumpAction;
    public event Action OnStartShootAction;
    public event Action OnFinishShootAction;
    private Vector3 _shootPoint;

    private Vector3 _rotation;
    private bool _isShooting;
    private bool _inShootingProcess;

    [Header("AI Data")]
    [SerializeField] private Transform _aimingPointAxis;
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private bool _isActive = true;
    [SerializeField] private float _shootDelayTime;
    [SerializeField] private float _movingRange;
    private Transform _targetPlayer;

    [SerializeField] private Vector3 _walkPoint;
    private Vector3 direction3 = Vector3.zero;

    public void SetTarget(Transform target)
    {
         _targetPlayer = target;
    }
    void Start()
    {
        _walkPoint = transform.position;
    }

    void Update()
    {
        Vector3 direction = (_targetPlayer.position) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        _rotation = rotation.eulerAngles;
        _shootPoint = _targetPlayer.position + Vector3.up;
        _aimingPointAxis.rotation = rotation;
        if (_isActive)
        {
            Ray ray = new Ray(_shootOrigin.position, (_targetPlayer.position + Vector3.up) - _shootOrigin.position);
            Moving();
            if (_inShootingProcess) return;
            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                if (hit.collider.TryGetComponent<Trigger>(out Trigger tr))
                {
                    _inShootingProcess = true;
                    StartCoroutine(DelayShoot());
                }
                else
                {
                    _isShooting = false;
                }
            }
            else
            {
                _isShooting = false;
            }
        }
    }

    private IEnumerator DelayShoot()
    {
        Debug.Log("Delay Shoot");
        _inShootingProcess = true;
        yield return new WaitForSeconds(_shootDelayTime);
        _isShooting = true;
        _inShootingProcess = false;
    }

    private void Moving()
    {
        if (_isShooting || _inShootingProcess)
        {
            OnDirectionUpdateAction?.Invoke(Vector2.zero);
            return;
        }
        if(_walkPoint == null || Math.Abs((_walkPoint - transform.position).magnitude) < 2)
        {
            SearchWalkPoint();
        }
        direction3 = _walkPoint - transform.position;
        Vector2 direction = new Vector2(direction3.x, direction3.z);
        Vector3 localDirection = transform.InverseTransformDirection(direction3);
        direction = new Vector2(localDirection.x, localDirection.z);
        OnDirectionUpdateAction?.Invoke(direction);
    }
    private void SearchWalkPoint()
    {
        _walkPoint = transform.position + new Vector3(UnityEngine.Random.Range(-_movingRange, _movingRange - 1), 0, UnityEngine.Random.Range(-_movingRange + 1, _movingRange));
        if(Physics.Raycast(transform.position + Vector3.up, (_walkPoint + Vector3.up) - (transform.position + Vector3.up),(_walkPoint - transform.position).magnitude))
        {
            SearchWalkPoint();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction3);
        Gizmos.DrawSphere(_walkPoint, 1);
    }

}
