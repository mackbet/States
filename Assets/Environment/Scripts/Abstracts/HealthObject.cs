using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthObject :  MonoBehaviour, IDamageable
{
    public Action OnDead;
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

    [SerializeField] protected int armor;

    public void TakeDamage(float value)
    {
        Health -= value * ((100f - armor) / 100f);
    }

    protected virtual void Die()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }

}
