using DG.Tweening;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _jumpForce;
    [SerializeField] float _moveLerpSpeed = 5f;
    [SerializeField] private Vector2 _lerpVector;
    [SerializeField] private bool _isGlobalMoving;
    private Rigidbody _rigidBody;

    float _rotationLerpSpeed = 0.5f;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 inputDirection, Vector3 cameraDirection)
    {
        _lerpVector = Vector2.Lerp(_lerpVector, inputDirection, _moveLerpSpeed * Time.deltaTime);
        Vector3 direction = new Vector3(_lerpVector.x, 0, _lerpVector.y);
        if (direction.magnitude > 0.1)
        {
            if (!_isGlobalMoving)
            {
                Vector2 convertDirection = ConvertDirection(new Vector2(direction.x, direction.z), cameraDirection);
                direction = new Vector3(convertDirection.x, 0, convertDirection.y);
            }
        }
        Vector3 localDirection = new Vector3(direction.x * _speed, _rigidBody.velocity.y, direction.z * _speed);
        _rigidBody.velocity = localDirection;
    }

    public void Rotate(Vector3 direction)
    {
        if (direction.magnitude > 0.1)
        {
            float angleY = Mathf.Atan(direction.x / direction.z);
            if (direction.z < 0) angleY += Mathf.PI;
            // _currentAngleY = Mathf.Lerp(_currentAngleY, angleY, _rotationLerpSpeed * Time.deltaTime);
            transform.DORotate(new Vector3(0, angleY * 180 / Mathf.PI, 0), _rotationLerpSpeed);
            //transform.eulerAngles = new Vector3(0, angleY * 180 / Mathf.PI, 0);
        }
    }
    private Vector2 ConvertDirection(Vector2 localDirection, Vector3 cameraDirection)
    {
        if (localDirection.magnitude == 0)
        {
            return new Vector2(0, 0);
        }
        Vector2 res = new Vector2();
        float angle = Mathf.Atan(localDirection.x / localDirection.y);
        if (localDirection.y < 0)
        {
            angle += Mathf.PI;
        }
        float applyAngle = Mathf.Atan(cameraDirection.x / cameraDirection.z);
        if (cameraDirection.z < 0)
        {
            applyAngle += Mathf.PI;
        }
        float globalAngle = applyAngle + angle;
        res.x = Mathf.Sin(globalAngle);
        res.y = Mathf.Cos(globalAngle);
        return res;
    }

    public void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
