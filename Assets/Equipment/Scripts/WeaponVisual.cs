using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    [SerializeField] private AttackController _attackController;
    [SerializeField] private Transform _container;
    private Transform _lastWeapon;

    private void OnEnable()
    {
        UpdateVisual(_attackController.Weapon);
        _attackController.OnWeaponChanged += UpdateVisual;
    }

    private void OnDisable()
    {
        _attackController.OnWeaponChanged -= UpdateVisual;
    }

    private void UpdateVisual(Weapon weapon)
    {
        if (_lastWeapon)
            Destroy(_lastWeapon.gameObject);

        _lastWeapon = Instantiate(weapon.Model, _container).transform;
    }
}
