using System;
using UnityEngine;

public class EnemyHealthSystem
{
    private float _maxHealth;
    private float _currentHp;

    public Action OnDemageEvent;
    public Action OnDeathEvent;

    public EnemyHealthSystem(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHp = maxHealth;
    }

    public float CurrentHp()
    {
        return _currentHp;
    }

    public float MaxHealth()
    {
        return _maxHealth;
    }

    // «б≥льшенн€ 
    public void IncreaseHealth(float value)
    {
        _currentHp += value;
    }

    // «меньшенн€
    public void DecreaseHealth(float value) 
    {
        _currentHp -= value;
        OnDemageEvent?.Invoke();
    }

    public bool IsAlive()
    {
        if (_currentHp <= 0f)
        {
            Debug.Log("Enemy is dead");
            OnDeathEvent?.Invoke();
            return false;
        }
        else
        {
            return true;
        };
    }
}
