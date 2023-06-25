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
    public bool IsRequirementsMet(Commonwealth ñommonwealth)
    {
        foreach (BuildRequirement requirement in _requirements)
        {
            if (ñommonwealth.GetItemCount(requirement.Item) < requirement.Count)
                return false;
        }

        return true;
    }
    public void GetResources(Commonwealth ñommonwealth)
    {
        foreach (BuildRequirement requirement in _requirements)
        {
            ñommonwealth.ReduceResource(requirement.Item, requirement.Count);
        }
    }
    
    [Serializable]
    private class BuildRequirement
    {
        [field: SerializeField] public AssetItem Item { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}
