using UnityEngine;


public class PlayerHealthSystem
{
    private float _maxHealth;
    private float _currentHp;

    public PlayerHealthSystem(float maxHealth)
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

    // Збільшення 
    public void IncreaseHealth(float value)
    {
        _currentHp += value;
    }

    // Зменьшення
    public void DecreaseHealth(float value)
    {
        _currentHp -= value;
    }

    public bool IsAlive()
    {
        if (_currentHp <= 0f) return false;
        else return true;
    }
}
