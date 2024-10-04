using System;
using UnityEngine;

public interface InputSystem
{
    public Vector3 ShootOrigin {  get; protected set; }
    public Vector3 ShootDirection { get; protected set; }
    public Vector3 CharacterRotation { get; protected set; }

    public bool IsShooting { get; protected set; }


    public event Action<Vector2> OnDirectionUpdateAction;
    public event Action OnJumpAction;
    public event Action OnStartShootAction;
    public event Action OnFinishShootAction;
}
