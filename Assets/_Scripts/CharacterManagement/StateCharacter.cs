using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCharacter
{
    public string Name => _stateName;
    protected StateMachine _stateMachine;
    protected CharacterManager _characterManager;
    protected AnimatorCharacter _animator;
    private
    protected string _stateName;

    public StateCharacter(CharacterManager characterManager, StateMachine stateMachine,AnimatorCharacter animator, string stateName)
    {
        _characterManager = characterManager;
        _stateMachine = stateMachine;
        _stateName = stateName;
        _animator = animator;
    }

    public abstract void UpdateState();
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void AnimationEvent(string parameter);
    public abstract void ActionHandler(string tag);
    public abstract void TriggerEnter(Collider collider);
}

