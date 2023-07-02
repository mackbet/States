using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController: MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private CharacterRotator _characterRotator;
    [field:SerializeField] public Weapon Weapon { get; private set; }

    public Action OnAttackStarted;
    public Action OnAttackDelayed;
    public Action OnAttackRecharged;

    public Action<Weapon> OnWeaponChanged;

    [SerializeField] private bool ready = true;

    private void Awake()
    {
        OnAttackDelayed += DealDamage;
    }

    public void TryToAttack(int ownerId)
    {
        if (ready)
        {
            ScanTargets(ownerId);
            StartCoroutine(Recharge());
            StartCoroutine(Delay());

            OnAttackStarted?.Invoke();
        }
    }
    public bool InAttackZone(Vector3 position)
    {
        float distance = Vector3.Distance(position, transform.position);

        return distance < Weapon.AttackRange;
    }
   
    public void SetWeapon(Weapon newWeapon)
    {
        Weapon = newWeapon;
        OnWeaponChanged.Invoke(newWeapon);
    }
    IEnumerator Delay()
    {
        DateTime start = DateTime.Now;

        yield return new WaitForSeconds(Weapon.AttackTime);
        OnAttackDelayed?.Invoke();
    }
    IEnumerator Recharge()
    {
        DateTime start = DateTime.Now;

        ready = false;
        yield return new WaitForSeconds(Weapon.AttackRecharge);
        ready = true;

        OnAttackRecharged?.Invoke();
    }
    List<IDamageable> targets = new List<IDamageable>();
    private void ScanTargets(int ownerId)
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, Weapon.AttackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                if (damageable.Parameters.TeamId == ownerId)
                    continue;

                Vector3 directionToTarget = collider.transform.position - _transform.position;
                _characterRotator.RotateInstantly(directionToTarget);

                float angle = Vector3.Angle(_transform.forward, directionToTarget);
                if (angle < Weapon.AttackAngle / 2)
                    targets.Add(damageable);
            }
        }
    }
    private void DealDamage()
    {
        foreach (IDamageable target in targets)
        {
            if(target.IsAlive)
                target.TakeDamage(Weapon.Damage);
        }
        
        targets.Clear();
    }
}
