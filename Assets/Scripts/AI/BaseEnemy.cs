using System;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IHitable
{
    [SerializeField] private float _maxHealth = 1f;
    [SerializeField] private float _destroyDelay;
    [SerializeField] private Collider2D _collider;
    private float _currentHealth;
    protected Strategy _currentStrategy;

    protected virtual void Awake()
    {
        _currentHealth = _maxHealth;
    }

    protected void SetStrategy(Strategy strategy)
    {
        if (_currentHealth == 0)
            return;
        _currentStrategy?.Exit(this);
        _currentStrategy = strategy;
        _currentStrategy?.Start(this);
    }

    protected virtual void LateUpdate()
    {
        if (_currentStrategy != null)
        {
            _currentStrategy.Update(this);
        }
    }
    public void Hit(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        _currentStrategy = null;
        _collider.enabled = false;
        Destroy(gameObject, _destroyDelay);
    }
}