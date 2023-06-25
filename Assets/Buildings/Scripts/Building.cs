using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : HealthObject
{
    [SerializeField] private List<BuildRequirement> _requirements = new List<BuildRequirement>();

    public Vector2Int indeces;

    protected override void Die()
    {
        base.Die();
    }
    [Serializable]
    private class BuildRequirement
    {
        [field: SerializeField] public AssetItem Item { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}
