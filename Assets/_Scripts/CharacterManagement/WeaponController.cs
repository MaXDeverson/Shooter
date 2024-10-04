using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponController : MonoBehaviour
{
    public bool IsReadyToShoot { get; private set; }

    [SerializeField] private MultiAimConstraint _ik_R;
    [SerializeField] private TwoBoneIKConstraint _ik_L;
   // [SerializeField] private MultiAimConstraint _headConstraint;
    [SerializeField] private MultiAimConstraint _bodyConstraint;
    [SerializeField] private RigBuilder _rigBuilder;
    [SerializeField] private Transform _l_IKTarget;
    [SerializeField] private bool _isActivate;
    [SerializeField] private float _weightChangeDeltaTime = 0.05f;
    [SerializeField] private float _weightChangeStep = 0.05f;
    [SerializeField] private float _weightChangeDecreaseStep = 0.05f;
    [SerializeField] private float _weightCoefficientBody;
    [SerializeField] private float _weightCoefficientHead;
    [Header("Shoot Animation")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] AnimationCurve _shootAnimationCurve;
    [SerializeField] AnimationCurve _shootBodyAnimationCurve;
    [SerializeField] float _shootFrameDeltaTime;
    [SerializeField] float _shootChangeStep;
    [SerializeField] float _curveMultiplicator;
    private float _lastShootTime;
    private float _animationCurvePoint;
    private Vector3 _ik_R_offset;
    private Vector3 _body_offset;

    private float _currentWeight = 0;
    private Coroutine _animationCoroutine;
    private bool _coroutineIsRunning = false;
    private bool _isShooting;

    private void Awake()
    {
        _ik_R_offset = _ik_R.data.offset;
        _body_offset = _bodyConstraint.data.offset;
    }

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        if(_lastShootTime + _weapon.ShootDelay < Time.time)
        {
            _lastShootTime = Time.time;
            _weapon.ApplyWeapon(origin, direction);
            StartCoroutine(ShootAnimation());
        }
    }
    private IEnumerator ShootAnimation()
    {
        while(_animationCurvePoint < 1)
        {
            yield return new WaitForSeconds(_shootFrameDeltaTime);
            _animationCurvePoint += _shootChangeStep;
            _ik_R.data.offset = _ik_R_offset + new Vector3(0, _shootAnimationCurve.Evaluate(_animationCurvePoint) * _curveMultiplicator, 0);
            _bodyConstraint.data.offset = _body_offset + new Vector3(0,0,_shootBodyAnimationCurve.Evaluate(_animationCurvePoint) * _curveMultiplicator);
            if(_animationCurvePoint >= 1)
            {
                _animationCurvePoint = 0;
                _ik_R.data.offset = _ik_R_offset;
                _bodyConstraint.data.offset = _body_offset;
                break;

            }
        }
        
    }
    public void StartShootMode()
    {
        if (_isShooting) return;
        _isShooting = true;
        if (_coroutineIsRunning)
        {
            StopCoroutine(_animationCoroutine);
            _coroutineIsRunning = false;
        }
        _animationCoroutine = StartCoroutine(WeightChange(true));
    }
    public void FinishShootMode()
    {
        if (!_isShooting) return;
        _isShooting = false;
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
        IsReadyToShoot = false;
        if (increase)
        {
            while (_currentWeight < 1)
            {
                _currentWeight += _weightChangeStep;
                if (_currentWeight > 1) _currentWeight = 1;
                _ik_R.weight = _currentWeight;
                _ik_L.weight = _currentWeight * 3;
                //_headConstraint.weight = _currentWeight * _weightCoefficientHead;
                _bodyConstraint.weight = _currentWeight * _weightCoefficientBody;
                yield return new WaitForSeconds(_weightChangeDeltaTime);

            }
            IsReadyToShoot = true;
        }
        else
        {
            while (_currentWeight > 0)
            {
                _currentWeight -= _weightChangeDecreaseStep;
                if (_currentWeight < 0) _currentWeight = 0;
                _ik_R.weight = _currentWeight;
                _ik_L.weight = _currentWeight * 3;
                //_headConstraint.weight = _currentWeight * _weightCoefficientHead;
                _bodyConstraint.weight = _currentWeight * _weightCoefficientBody;
                yield return new WaitForSeconds(_weightChangeDeltaTime);


            }

        }
        _coroutineIsRunning = false;
    }
    private float GetDirection(Vector3 direction, bool isCorrect)
    {
        return Mathf.Atan((direction.y / direction.x) * (isCorrect ? 1 : -1));
    }
    
}