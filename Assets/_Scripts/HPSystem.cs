using System;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    public bool IsDied { get => _isDied; }
    public Action OnDie;
    public Action<int,int, float> OnDamaged;

    [SerializeField] private int _maxHP = 100;
    [SerializeField] private List<ArmoredPart> _armoredParts;
    [SerializeField] private int _currentHP;

    private bool _isDied;
    private void Awake()
    {
        _currentHP = _maxHP;
        foreach (var item in _armoredParts)
        {
            item.Init();
            item.OnDamaged += OnDamage;
        }
    }

    private void OnDamage(Trigger trigger, int value)
    {
        if (_isDied) return;
        _currentHP -= value;
        if(_currentHP <= 0)
        {
            _isDied = true;
            _currentHP = 0;
            OnDie?.Invoke();
        }
        OnDamaged?.Invoke(value, _currentHP, (float)_currentHP / _maxHP);

    }

    [Serializable]
    private class ArmoredPart
    {
        public Action<Trigger, int> OnDamaged;
        public Trigger PartTrigger { get => _partTrigger; }
        public float Armor { get => _armor; }

        [SerializeField] private Trigger _partTrigger;
        [SerializeField] private float _armor;

        public void Init()
        {
            PartTrigger.OnTriggerAction += (string tag, int value) =>
            {
                int calculateValue = value - (int)(value * _armor);
                OnDamaged?.Invoke(PartTrigger, calculateValue);
            };
        }
    }
}
