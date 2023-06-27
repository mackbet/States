
using System.Collections.Generic;
using UnityEngine;

public class MilitaryPart : MonoBehaviour
{
    public List<Barracks> BuiltBarracks => _builtBarracks;

    [SerializeField] private List<Barracks> _builtBarracks;

    public void AddHouse(Barracks barracks)
    {
        _builtBarracks.Add(barracks);
        barracks.OnDead += () => RemoveHouse(barracks);
    }
    public void RemoveHouse(Barracks barracks)
    {
        _builtBarracks.Remove(barracks);
    }
}
