using System.Collections.Generic;
using UnityEngine;

public class AnimatorCharacter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movingVectorLerp;
    private string[] _animationStateNames = new string[5] { "idle", "run", "jump", "fall", "landing" };
    [Header("Ragdoll")]
    [SerializeField] private List<Transform> _bodyParts;
    [SerializeField] private Collider _mainCollider;
    [SerializeField] private Rigidbody _mainRigidBody;

    private Vector2 _currentLerpVector;

    public void UpdateDirection(Vector2 direction)
    {
        _currentLerpVector = Vector2.Lerp(_currentLerpVector, direction, _movingVectorLerp * Time.deltaTime);
        _animator.SetFloat("directionX", _currentLerpVector.x);
        _animator.SetFloat("directionY", _currentLerpVector.y);
    }
    public void PlayRagdoll()
    {
        _animator.enabled = false;
        _mainCollider.isTrigger = true;
        _mainRigidBody.isKinematic = true;
        for(int i = 0;i< _bodyParts.Count;i++)
        {
            Transform part = _bodyParts[i];
            part.GetComponent<Collider>().isTrigger = false;
            part.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public void PlayAnimation(AnimationType type)
    {
        ResetBools();
        switch (type)
        {
            case AnimationType.Run:
                _animator.SetBool(_animationStateNames[(int)type], true);
                break;
            case AnimationType.Idle:
                _animator.SetBool(_animationStateNames[(int)type], true);
                break;
            default:
                _animator.SetTrigger(_animationStateNames[(int)type]);
                break;
        }
    }

    private void ResetBools()
    {
        _animator.SetBool(_animationStateNames[(int)AnimationType.Idle],false);
        _animator.SetBool(_animationStateNames[(int)AnimationType.Run], false);
    }
}

public enum AnimationType
{
    Idle,
    Run,
    Jump,
    Fall,
    Landing,
}
