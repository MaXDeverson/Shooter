using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private StateCharacter _currentState;
    public string CurrentStateName => _currentState.Name;
    public Vector2 InputDirection => _playerInputData.Direction;
    public InputSystem Input => _input;
    private PlayerInputData _playerInputData;
    private InputSystem _input;

    public void SetStartState(StateCharacter state)
    {
        _currentState = state;
        _currentState.EnterState();
    }
    public StateMachine(InputSystem input)
    {
        _playerInputData = new PlayerInputData();
        _input = input;
        input.OnDirectionUpdateAction += (direction) =>
        {
            _playerInputData.UpdateDirection(direction);
        };
       
    }
    public void InitStates( List<StateCharacter> statesForInitialization)
    {
        foreach (var item in statesForInitialization)
        {
            StateInit(_input, item);
        }
    }
    private void StateInit(InputSystem input, StateCharacter state)
    {
        input.OnJumpAction += () =>
        {
            state.ActionHandler("jump");
        };
        input.OnStartShootAction += () =>
        {
            state.ActionHandler("startShoot");
        };
        input.OnFinishShootAction += () =>
        {
            state.ActionHandler("finishSoot");
        };
    }
    public void UpdateCurrentState()
    {
        _currentState.UpdateState();
    }

    public void SwitchState(StateCharacter state)
    {
        _currentState.ExitState();
        _currentState = state;
        _currentState.EnterState();
    }

    public void AnimationEvent(string parameter)
    {
        Debug.Log("State Machine AnimEvent:" + parameter);
        _currentState.AnimationEvent(parameter);
    }
}

public class PlayerInputData
{
    public Vector2 Direction => _direction;

    private Vector2 _direction;

    public void UpdateDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
