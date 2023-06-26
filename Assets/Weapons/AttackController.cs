using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController: MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private CharacterRotator _characterRotator;
    [field:SerializeField] public Weapon Weapon { get; private set; }

    public Action onAttackStarted;
    public Action onAttackDelayed;
    public Action onAttackRecharged;

    [SerializeField] private bool ready = true;

    private void Awake()
    {
        onAttackDelayed += DealDamage;
    }

    public void TryToAttack(int ownerId)
    {
        if (ready)
        {
            ScanTargets(ownerId);
            StartCoroutine(Recharge());
            StartCoroutine(Delay());

            onAttackStarted?.Invoke();
        }
    }
    IEnumerator Delay()
    {
        DateTime start = DateTime.Now;

        yield return new WaitForSeconds(Weapon.attackTime);
        onAttackDelayed?.Invoke();
    }
    IEnumerator Recharge()
    {
        DateTime start = DateTime.Now;

        ready = false;
        yield return new WaitForSeconds(Weapon.attackRecharge);
        ready = true;

        onAttackRecharged?.Invoke();
    }


    List<IDamageable> targets = new List<IDamageable>();
    private void ScanTargets(int ownerId)
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, Weapon.attackRange);
        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null && damageable.Parameters.TeamId != ownerId)
            {
                Vector3 directionToTarget = collider.transform.position - _transform.position;
                _characterRotator.RotateInstantly(directionToTarget);

                float angle = Vector3.Angle(_transform.forward, directionToTarget);
                if (angle < Weapon.attackAngle / 2)
                    targets.Add(damageable);
            }
        }
    }
    private void DealDamage()
    {
        foreach (IDamageable target in targets)
        {
            if(target.IsAlive)
                target.TakeDamage(Weapon.damage);
        }
        
        targets.Clear();
    }
    public bool InAttackZone(Vector3 position)
    {
        float distance = Vector3.Distance(position, transform.position);

        return distance < Weapon.attackRange;
    }
}
