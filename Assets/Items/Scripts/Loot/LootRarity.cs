using System.Collections.Generic;
using UnityEngine;
public enum Rarity
{
    Common,
    Rare,
    Mythical,
    Legendary,
    Immortal
}

[CreateAssetMenu(menuName = "ScriptableObject/LootRarity")]
public class LootRarity : ScriptableObject
{
    [SerializeField] List<RarityColor> rarityColors;

    public Color GetColor(Rarity rarity)
    {
        return rarityColors[rarityColors.FindIndex(x => x.Rarity == rarity)].Color;
    }

    [System.Serializable]
    private class RarityColor
    {
        [field:SerializeField] public Rarity Rarity { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }

}
