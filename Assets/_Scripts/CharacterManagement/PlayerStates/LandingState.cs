using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : StateCharacter
{
 
    public override void EnterState()
    {
        _animator.PlayAnimation(AnimationType.Landing);

    }

    public override void ExitState()
    {

    }

    public override void TriggerEnter(Collider collider)
    {

    }

    public override void UpdateState()
    {

    }

    public override void ActionHandler(string tag)
    {
        switch (tag)
        {
            case "jump":

                break;
        }
    }
    public override void AnimationEvent(string parameter)
    {
        switch(parameter)
        {
            case "landingFinished":
                Debug.Log("landing Finished");
                if(_stateMachine.InputDirection.magnitude > 0)
                {
                    _stateMachine.SwitchState(_characterManager.RunState);
                }
                else
                {
                    _stateMachine.SwitchState(_characterManager.IdleState);
                }
                break;
        }
    }



    public LandingState(CharacterManager manager, StateMachine machine, AnimatorCharacter animator, string name) : base(manager, machine, animator, name)
    {

    }
}