using System;
using UnityEngine;
using Zenject;

public class PCInputSystem : MonoBehaviour, InputSystem
{
    [Inject] CameraController _camera;
    public event Action<Vector2> OnDirectionUpdateAction;
    public event Action OnJumpAction;
    public event Action OnStartShootAction;
    public event Action OnFinishShootAction;

    Vector3 InputSystem.ShootOrigin { get => _camera.AimPosition;  set => throw new NotImplementedException(); }
    Vector3 InputSystem.ShootDirection { get => _camera.Direction; set => throw new NotImplementedException(); }
    Vector3 InputSystem.CharacterRotation { get => _camera.Rotation; set => throw new NotImplementedException(); }
    bool InputSystem.IsShooting { get => _isShooting; set => throw new NotImplementedException(); }

    private bool _isShooting;

    void Update()
    {
        Vector2 direction = Vector2.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpAction?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnStartShootAction?.Invoke();
        }
        if(_isShooting && Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnFinishShootAction?.Invoke();
        }
        _isShooting = Input.GetKey(KeyCode.Mouse0);
        OnDirectionUpdateAction?.Invoke(direction);
    }
}
