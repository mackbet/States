using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : HealthObject
{
    [field:SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    [SerializeField] private List<BuildRequirement> _requirements = new List<BuildRequirement>();

    public Vector2Int indeces;

    public void Initialize(Parameters parameters)
    {
        _parameters = parameters;

        MeshRenderer.material = Parameters.BuildingMaterial;
    }

    protected override void Die()
    {
        base.Die();
    }
    public bool IsRequirementsMet(Commonwealth ˝ommonwealth)
    {
        foreach (BuildRequirement requirement in _requirements)
        {
            if (˝ommonwealth.GetItemCount(requirement.Item) < requirement.Count)
                return false;
        }

        return true;
    }
    public void GetResources(Commonwealth ˝ommonwealth)
    {
        foreach (BuildRequirement requirement in _requirements)
        {
            ˝ommonwealth.ReduceResource(requirement.Item, requirement.Count);
        }
    }
    
    [Serializable]
    private class BuildRequirement
    {
        [field: SerializeField] public AssetItem Item { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}
