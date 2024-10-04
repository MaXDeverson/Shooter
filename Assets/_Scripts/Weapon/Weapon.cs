using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float ShootDelay { get => _shootDelay; }
    [SerializeField] protected Transform _rightHandTransform;
    [SerializeField] protected Transform _leftHandTransform;
    [SerializeField] protected int _damage;
    [SerializeField] protected bool _isOneHandType;
    [SerializeField] protected float _shootDelay;
    public abstract void ApplyWeapon(Vector3 origin, Vector3 direction);
}
