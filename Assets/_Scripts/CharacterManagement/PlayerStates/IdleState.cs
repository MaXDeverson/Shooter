using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateCharacter
{
    public override void EnterState()
    {
        _animator.PlayAnimation(AnimationType.Idle);
        _characterManager.SetInShootMode(true);
    }

    public override void ExitState()
    {

    }

    public override void TriggerEnter(Collider collider)
    {

    }

    public override void UpdateState()
    {
        if (_characterManager.IsGrounded())
        {
            if (_stateMachine.InputDirection.magnitude > 0.3)
            {
                _stateMachine.SwitchState(_characterManager.RunState);
            }
            _characterManager.Move(new Vector2(0, 0));
        }
        else
        {
            _stateMachine.SwitchState(_characterManager.FallState);
        }
        if(_stateMachine.Input.IsShooting)
        {
            _characterManager.Shoot();
        }
      
    }

    public override void ActionHandler(string tag)
    {
        switch (tag)
        {
            case "jump":
                _stateMachine.SwitchState(_characterManager.JumpState);
                break;
        }
    }

    public override void AnimationEvent(string parameter)
    {

    }


    public IdleState(CharacterManager manager, StateMachine machine, AnimatorCharacter animator, string name) : base(manager, machine,animator, name)
    {

    }
}
