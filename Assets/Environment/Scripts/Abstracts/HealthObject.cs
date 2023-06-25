using System;
using UnityEngine;

public abstract class HealthObject :  MonoBehaviour, IDamageable
{
    public int TeamId => teamId;
    public Action OnDead;
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
    [SerializeField] private int teamId;

    [SerializeField] protected int armor;

    public void TakeDamage(float value)
    {
        if (immortal)
            return;

        Health -= value * ((100f - armor) / 100f);
    }

    protected virtual void Die()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }

}
