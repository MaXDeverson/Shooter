using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineFreeLook _cineMachine;
    [SerializeField] private Transform _aimSprite;
    public Vector3 AimPosition => _camera.ScreenToWorldPoint(_aimSprite.position);
    [Header("Aiming")]
    [SerializeField] private float _defaultFOVValue;
    [SerializeField] private float _aimingFOVValue;
    [SerializeField] private float _weightChangeDeltaTime = 0.05f;
    [SerializeField] private float _weightChangeStep = 0.05f;

    private bool _isAiming = true;

    public bool IsAiming { get => _isAiming; }
    [SerializeField] private float _currentWeight = 0;
    private Coroutine _animationCoroutine;
    private bool _coroutineIsRunning = false;


    public Vector3 Direction
    {
        get
        {
            return _camera.transform.forward;
        }
    }
    public Vector3 Rotation
    {
        get
        {
            return _camera.transform.eulerAngles;
        }
    }
    void Start()
    {
        _currentWeight = _defaultFOVValue;
        
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            EnableAimingMode(true);
        }
        if(Input.GetMouseButtonUp(1))
        {
            EnableAimingMode(false);
        }
    }
    public void EnableAimingMode(bool enable)
    {
        if (enable)
        {
            Decrease();
        }
        else
        {
            Increase();
        }
    }

    private void Increase()
    {
        if (_isAiming) return;
        _isAiming = true;
        if (_coroutineIsRunning)
        {
            StopCoroutine(_animationCoroutine);
            _coroutineIsRunning = false;
        }
        _animationCoroutine = StartCoroutine(WeightChange(true));
    }

    private void Decrease()
    {
        if (!_isAiming) return;
        _isAiming = false;
        if (_coroutineIsRunning)
        {
            StopCoroutine(_animationCoroutine);
            _coroutineIsRunning = false;
        }
        _animationCoroutine = StartCoroutine(WeightChange(false));
    }

    private IEnumerator WeightChange(bool increase)
    {
        _coroutineIsRunning = true;
        if (increase)
        {
         
            while (_currentWeight < _defaultFOVValue)
            {
                _currentWeight += _weightChangeStep;
                if (_currentWeight > _defaultFOVValue) _currentWeight = _defaultFOVValue;
                _cineMachine.m_Lens.FieldOfView = _currentWeight;
                yield return new WaitForSeconds(_weightChangeDeltaTime);

            }
        }
        else
        {
            while (_currentWeight > _aimingFOVValue)
            {
                _currentWeight -= _weightChangeStep;
                if (_currentWeight < _aimingFOVValue) _currentWeight =_aimingFOVValue;
                _cineMachine.m_Lens.FieldOfView = _currentWeight;
                yield return new WaitForSeconds(_weightChangeDeltaTime);
            }

        }
        _coroutineIsRunning = false;
    }
}
