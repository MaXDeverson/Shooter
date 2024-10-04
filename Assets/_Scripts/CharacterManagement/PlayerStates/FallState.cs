using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : StateCharacter
{
    public override void AnimationEvent(string parameter)
    {

    }

    public override void EnterState()
    {
        _animator.PlayAnimation(AnimationType.Fall);
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
            _stateMachine.SwitchState(_characterManager.LandingState);
            _characterManager.PhysicsLanding();
        }
        if (_stateMachine.Input.IsShooting)
        {
            _characterManager.Shoot();
        }
    }

    public override void ActionHandler(string tag)
    {
        switch (tag)
        {
            case "jump":

                break;
        }
    }

    public FallState(CharacterManager manager, StateMachine machine, AnimatorCharacter animator, string name) : base(manager, machine,animator, name)
    {

    }
}
