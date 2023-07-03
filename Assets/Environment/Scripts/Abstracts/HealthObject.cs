using System;
using UnityEngine;

public abstract class HealthObject : MonoBehaviour, IDamageable
{
    public Parameters Parameters => _parameters;

    public Action OnDead;
    public Action<float> OnHealthChanged;
    public bool immortal;
    public float Health {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
                Die();
        }
    }
    [SerializeField] private float health;

    public bool IsAlive => isAlive;
    private bool isAlive => Health > 0;

    [SerializeField] protected int _armor;
    [SerializeField] protected Parameters _parameters;

    public void TakeDamage(float value)
    {
        if (immortal)
            return;

        Health -= value * ((100f - _armor) / 100f);

        OnHealthChanged?.Invoke(Health);
    }
    protected virtual void Die()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }

}
