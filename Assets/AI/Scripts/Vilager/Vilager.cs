using System;
using UnityEngine;

public class Vilager : HealthObject
{
    [field: SerializeField] public Panoply Panoply { get; private set; }
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
