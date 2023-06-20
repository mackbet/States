using UnityEngine;

public interface IItem
{
    public string Name { get; }
    public Sprite UIIcon { get; }
    public Rarity Rarity { get; }
}
