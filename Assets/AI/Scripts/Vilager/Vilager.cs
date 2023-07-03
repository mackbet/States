using System;
using UnityEngine;

public class Vilager : HealthObject
{
    [field: SerializeField] public Panoply Panoply { get; private set; }
    [field: SerializeField] public Specialization Specialization { get; private set; }
    public void SetSpecialization(Specialization newSpecialization)
    {
        Specialization = newSpecialization;
    }
    private void Awake()
    {
        _parameters = Instantiate(_parameters);
    }
    private void Start()
    {
        if (Panoply)
        {
            _armor = Panoply.Armor;
            OnPanoplyChanged.Invoke(Panoply);
        }
    }
    public Action<Panoply> OnPanoplyChanged;
}
