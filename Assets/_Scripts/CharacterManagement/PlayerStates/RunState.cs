using UnityEngine;

public class RunState : StateCharacter
{
    public override void EnterState()
    {
        _animator.PlayAnimation(AnimationType.Run);
        _characterManager.SetInShootMode(false);
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
            if (_stateMachine.InputDirection.magnitude < 0.3)
            {
                _stateMachine.SwitchState(_characterManager.IdleState);
            }
            _characterManager.Move(_stateMachine.InputDirection);
            _animator.UpdateDirection(_stateMachine.InputDirection.normalized);
        }
        else
        {
            _stateMachine.SwitchState(_characterManager.FallState);
        }
    }
    public override void AnimationEvent(string parameter)
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

    public RunState(CharacterManager manager, StateMachine machine, AnimatorCharacter animator, string name) : base(manager, machine, animator, name)
    {

    }
}
