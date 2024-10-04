using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : StateCharacter
{
    public override void AnimationEvent(string parameter)
    {

    }

    public override void EnterState()
    {
        _characterManager.Jump();
        _animator.PlayAnimation(AnimationType.Jump);
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
            //Write!!
        }
        else
        {
            _stateMachine.SetStartState(_characterManager.FallState);
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

    public JumpState(CharacterManager manager, StateMachine machine, AnimatorCharacter animator, string name) : base(manager, machine, animator, name)
    {

    }
}
