using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterManager : MonoBehaviour
{
    [Inject] private InputSystem _inputController;
    [SerializeField] private BotInputSystem _botInputSystem;

    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private AnimatorCharacter _characterAnimator;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private HPSystem _hpSystem;
    [SerializeField] private AnimationEventListener _animationEventListener;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _groundedPoint;
    [SerializeField] private float _groundedR;
    [SerializeField] private string _currentStateName;
    private StateMachine _stateMachine;

    public StateCharacter IdleState;
    public StateCharacter RunState;
    public StateCharacter FallState;
    public StateCharacter JumpState;
    public StateCharacter LandingState;

    private void Awake()
    {
        if (_botInputSystem != null)
        {
            _inputController = _botInputSystem;
        }
        StateMachineInit();
        _animationEventListener.AnimationEventInvoke += AnimationEvent;
        _hpSystem.OnDie += _characterAnimator.PlayRagdoll;
    }
    private void Update()
    {
        _currentStateName = _stateMachine.CurrentStateName;
        if (!_hpSystem.IsDied)
        {
            _stateMachine.UpdateCurrentState();
            transform.eulerAngles = new Vector3(0, _inputController.CharacterRotation.y, 0);
        }
    }
    private void StateMachineInit()
    {
        _stateMachine = new StateMachine(_inputController);
        IdleState = new IdleState(this, _stateMachine,_characterAnimator, "idle");
        RunState = new RunState(this, _stateMachine,_characterAnimator,"fun");
        FallState = new FallState(this, _stateMachine,_characterAnimator, "fall");
        JumpState = new JumpState(this, _stateMachine, _characterAnimator, "jump");
        LandingState = new LandingState(this, _stateMachine,_characterAnimator, "landing");
        _stateMachine.InitStates(new List<StateCharacter>() { IdleState, RunState, FallState, JumpState, LandingState });
        _stateMachine.SetStartState(IdleState);
    }

    public void Move(Vector2 direction)
    {
        _characterMover.Move(direction,_inputController.ShootDirection);
    }
    public void Jump()
    {
        _characterMover.Jump();
    }
    public void Shoot()
    {
        _weaponController.Shoot(_inputController.ShootOrigin,_inputController.ShootDirection);
    }
    public bool IsGrounded()
    {
        Collider [] colliders = Physics.OverlapSphere(_groundedPoint.position, _groundedR);
        for(int i = 0;i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag(Tag.Player))
            {
                return true;
            }
        }
        return false;
    }
    public void PhysicsLanding()
    {
        _rigidBody.velocity = Vector3.zero;
    }
    public void AnimationEvent(string parameter)
    {
        _stateMachine.AnimationEvent(parameter);
    }
    public void SetInShootMode(bool enable)
    {
        if (enable)
        {
            _weaponController.StartShootMode();
        }
        else
        {
            _weaponController.FinishShootMode();
        }
    }
}

public static class Tag
{
    public static string Player = "Player";
}